using System.Collections.Generic;
using System.Net;
using AW.Utilities.Commands.Interfaces;
using AW.Utilities.Validation.Interfaces;
using DvlaInfoApi.Dvla.UK.Commands;
using DvlaInfoApi.Framework.Models;
using DvlaInfoApi.Queries;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DvlaInfoApi.Functions
{
  public class VehicleUpdaterhttpTester
  {
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly ICommandHandlerAsync<VehicleEnquiryCommand> _dvlaGetVehicleHandler;
    private readonly IInformationValidator<Vehicle> _vehicleValidator;

    public VehicleUpdaterhttpTester(ILoggerFactory loggerFactory, IMediator mediator,
          ICommandHandlerAsync<VehicleEnquiryCommand> dvlaGetVehicleHandler,
          IInformationValidator<Vehicle> vehicleValidator)
    {
      _logger = loggerFactory.CreateLogger<VehicleUpdaterFunction>();
      _mediator = mediator;
      _dvlaGetVehicleHandler = dvlaGetVehicleHandler;
      _vehicleValidator = vehicleValidator;
    }

    [Function("vehicle-updater-test")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
      _logger.LogInformation($"C# Timer trigger tester (http) function executed at: {DateTime.Now}");
      
      var @event = new GetAllDvlaInfoEvent();
      var existing = await _mediator.Send(@event);

      if (existing is null || !existing.Any())
      {
        _logger.LogInformation($"Could not get any vehicles on: {DateTimeOffset.Now}");
        return req.CreateResponse(HttpStatusCode.BadRequest);
      }

      int recordsUpdated = 0;
      foreach (var vehicle in existing)
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
        if (info.Result == AW.Utilities.Validation.Enums.Result.Invalid)
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
        var saveEvent = new Commands.SaveVehicleRequest(vehicle);
        var updated = await _mediator.Send(saveEvent);

        if (updated is not null) recordsUpdated++;
      }

      _logger.LogInformation($"{recordsUpdated} vehicles updated from {existing.Count()}");

      var response = req.CreateResponse(HttpStatusCode.OK);
      await response.WriteAsJsonAsync(new
            {
              Updated = recordsUpdated
            });
      return response;
    }
  }
}
