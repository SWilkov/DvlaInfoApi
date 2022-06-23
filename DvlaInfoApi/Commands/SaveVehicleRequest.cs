using DvlaInfoApi.Framework.Models;
using MediatR;

namespace DvlaInfoApi.Commands
{
  public class SaveVehicleRequest : IRequest<Vehicle>
  {
    public Vehicle Vehicle { get; private set; }
    public SaveVehicleRequest(Vehicle vehicle) => this.Vehicle = vehicle;
  }
}
