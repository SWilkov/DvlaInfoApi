using System.Collections.Generic;
using System.Net;
using AW.Utilities.Commands.Interfaces;
using DvlaInfoApi.DataObjects;
using DvlaInfoApi.Dvla.UK.Commands;
using DvlaInfoApi.Queries;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DvlaInfoApi.Functions
{
  public class VehicleEnquiryFunction
  {
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly ICommandHandlerAsync<VehicleEnquiryCommand> _dvlaGetVehicleHandler;
    
    public VehicleEnquiryFunction(ILoggerFactory loggerFactory,
      IMediator mediator,
      ICommandHandlerAsync<VehicleEnquiryCommand> dvlaGetVehicleHandler)
    {
      _logger = loggerFactory.CreateLogger<VehicleEnquiryFunction>();
      _mediator = mediator;
      _dvlaGetVehicleHandler = dvlaGetVehicleHandler;
    }

    [Function("vehicle-enquiry")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
      _logger.LogInformation("C# HTTP trigger function processed a request.");

      var data = req.FunctionContext.BindingContext.BindingData;
      if (data == null)
      {
        var bad = req.CreateResponse(HttpStatusCode.BadRequest);
        return bad;
      }

      var registration = req.FunctionContext.BindingContext.BindingData["registration"];
      if (registration is null || string.IsNullOrWhiteSpace(registration.ToString()))
      {
        var noReg = req.CreateResponse(HttpStatusCode.BadRequest);
        noReg.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        noReg.WriteString("No vehicle registration added!");
        return noReg;
      }

      var getVehicleEvent = new GetDvlaInfoEvent(registration.ToString());
      var vehicle = await _mediator.Send(getVehicleEvent);
      if (vehicle is not null)
      {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync<VehicleResponseDto>(new VehicleResponseDto("SUCCESS", vehicle));
        return response;
      }

      //Get data from DVLA gov uk
      var cmd = new VehicleEnquiryCommand(registration.ToString());
      await _dvlaGetVehicleHandler.ExecuteAsync(cmd);

      if (cmd is null || cmd.Vehicle is null)
      {
        //TODO log
        var dvlaError = req.CreateResponse(HttpStatusCode.NotFound);
        dvlaError.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        dvlaError.WriteString("No vehicle info found on DVLA!");
        return dvlaError;
      }

      //save to db
      var saveEvent = new Commands.SaveVehicleRequest(cmd.Vehicle);
      var updated = await _mediator.Send(saveEvent);

      if (updated is not null)
      {
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync<VehicleResponseDto>(new VehicleResponseDto("SUCCESS", updated));
        return response;
      }

      var saveError = req.CreateResponse(HttpStatusCode.InternalServerError);
      return saveError;
    }
  }
}
