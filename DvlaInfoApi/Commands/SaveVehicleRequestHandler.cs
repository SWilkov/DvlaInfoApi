using DvlaInfoApi.DataLayer.Interfaces;
using DvlaInfoApi.Framework.Models;
using MediatR;

namespace DvlaInfoApi.Commands
{
  public class SaveVehicleRequestHandler : IRequestHandler<SaveVehicleRequest, Vehicle>
  {
    private readonly IRepository<Vehicle> _repository;

    public SaveVehicleRequestHandler(IRepository<Vehicle> repository)
    {
      _repository = repository;
    }
    public async Task<Vehicle> Handle(SaveVehicleRequest request, CancellationToken cancellationToken)
    {
      if (request is null) throw new ArgumentNullException(nameof(request));
      if (request.Vehicle is null) throw new ArgumentNullException(nameof(request.Vehicle));

      return await _repository.Save(request.Vehicle);      
    }
  }
}
