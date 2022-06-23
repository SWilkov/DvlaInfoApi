using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.Dvla.UK.Commands
{
  public class VehicleEnquiryCommand 
  {
    public string Registration { get; private set; }
    public Vehicle Vehicle { get; set; }
    public VehicleEnquiryCommand(string registration) => Registration = registration;
  }
}
