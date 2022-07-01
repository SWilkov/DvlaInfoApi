using DvlaInfoApi.Dvla.UK.Commands;
using DvlaInfoApi.Framework.Models;
using DvlaInfoApi.Queries;
using DvlaInfoApi.Utils.Interfaces;
using DvlaInfoApi.Utils.Validation.Enums;
using DvlaInfoApi.Utils.Validation.Interfaces;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DvlaInfoApi.Functions
{
  public class VehicleUpdaterFunction
  {
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly ICommandHandlerAsync<VehicleEnquiryCommand> _dvlaGetVehicleHandler;
    private readonly IInformationValidator<Vehicle> _vehicleValidator;

    public VehicleUpdaterFunction(ILoggerFactory loggerFactory, IMediator mediator,
          ICommandHandlerAsync<VehicleEnquiryCommand> dvlaGetVehicleHandler,
          IInformationValidator<Vehicle> vehicleValidator)
    {
      _logger = loggerFactory.CreateLogger<VehicleUpdaterFunction>();
      _mediator = mediator;
      _dvlaGetVehicleHandler = dvlaGetVehicleHandler;
      _vehicleValidator = vehicleValidator;
    }

    /// <summary>
    /// Making sure the data we store has all the latest info from the Dvla
    /// </summary>
    /// <param name="myTimer"></param>
    /// <returns></returns>
    [Function("vehicle-updater")]
    public async Task Run([TimerTrigger("0 0 0 * * *")] MyInfo myTimer)
    {
      _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
      _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

      var @event = new GetAllDvlaInfoEvent();
      var existing = await _mediator.Send(@event);

      if (existing is null || !existing.Any())
      {
        _logger.LogInformation($"Could not get any vehicles on: {DateTimeOffset.Now}");
        return;
      }

      int recordsUpdated = 0;
      foreach(var vehicle in existing)
      {
        //TODO validate here!
        if (string.IsNullOrWhiteSpace(vehicle.Registration))
        {
          _logger.LogInformation($"Could not get registration!");
          continue;
        }

        //Get data from DVLA gov uk
        var cmd = new VehicleEnquiryCommand(vehicle.Registration);
        await _dvlaGetVehicleHandler.ExecuteAsync(cmd);

        var info = _vehicleValidator.Validate(cmd.Vehicle);
        if (info.Result == Result.Invalid)
        {
          _logger.LogWarning($"{cmd.Vehicle.Registration} Failed Vehicle validation with error(s): {info.Message}");
          continue; 
        }

        //Map new info to existing vehicle and update
        vehicle.MotExpiryDate = cmd.Vehicle.MotExpiryDate;
        vehicle.MotStatus = cmd.Vehicle.MotStatus ?? "";
        vehicle.Color = cmd.Vehicle.Color;
        vehicle.MonthRegistration = cmd.Vehicle.MonthRegistration;
        vehicle.EngineCapacity = cmd.Vehicle.EngineCapacity;
        vehicle.FuelType = cmd.Vehicle.FuelType;
        vehicle.Make = cmd.Vehicle.Make;
        vehicle.DvlaInfo.Co2Emissions = cmd.Vehicle.DvlaInfo.Co2Emissions;
        vehicle.DvlaInfo.DateOfLastV5CIssued = cmd.Vehicle.DvlaInfo.DateOfLastV5CIssued;
        vehicle.DvlaInfo.MarkedForExport = cmd.Vehicle.DvlaInfo.MarkedForExport;
        vehicle.DvlaInfo.TaxDueDate = cmd.Vehicle.DvlaInfo.TaxDueDate;
        vehicle.DvlaInfo.TaxStatus = cmd.Vehicle.DvlaInfo.TaxStatus ?? "";
        vehicle.DvlaInfo.Wheelplan = cmd.Vehicle.DvlaInfo.Wheelplan ?? "";
        vehicle.DvlaInfo.TypeApproval = cmd.Vehicle.DvlaInfo.TypeApproval ?? "";

        //save to db
        var saveEvent = new Commands.SaveVehicleRequest(cmd.Vehicle);
        var updated = await _mediator.Send(saveEvent);

        if (updated is not null) recordsUpdated++;
      }

      _logger.LogInformation($"{recordsUpdated} vehicles updated from {existing.Count()}");
    }
  }

  public class MyInfo
  {
    public MyScheduleStatus ScheduleStatus { get; set; }

    public bool IsPastDue { get; set; }
  }

  public class MyScheduleStatus
  {
    public DateTime Last { get; set; }

    public DateTime Next { get; set; }

    public DateTime LastUpdated { get; set; }
  }
}
