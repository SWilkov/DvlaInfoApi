using AW.Utilities.Commands.Interfaces;
using DvlaInfoApi.Dvla.UK.Services;

namespace DvlaInfoApi.Dvla.UK.Commands
{
  public class VehicleEnquiryHandler : ICommandHandlerAsync<VehicleEnquiryCommand>
  {
    private readonly DvlaGovService _dvlaGovService;
    public VehicleEnquiryHandler(DvlaGovService dvlaGovService)
    {
      _dvlaGovService = dvlaGovService;
    }
    public async Task ExecuteAsync(VehicleEnquiryCommand command)
    {
      if (command == null) throw new ArgumentNullException(nameof(command));
      if (string.IsNullOrEmpty(command.Registration)) throw new ArgumentNullException(nameof(command.Registration));

      var vehicleInfo = await _dvlaGovService.Get(new Models.DvlaRequest
      {
        RegistrationNumber = command.Registration
      });

      if (vehicleInfo == null)
      {
        //TODO log this to App Insights
      }

      command.Vehicle = vehicleInfo;
    }
  }
}
