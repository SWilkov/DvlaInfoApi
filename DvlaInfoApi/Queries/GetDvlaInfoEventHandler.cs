using DvlaInfoApi.DataLayer.Interfaces;
using DvlaInfoApi.Framework.Models;
using MediatR;

namespace DvlaInfoApi.Queries
{
  public class GetDvlaInfoEventHandler : IRequestHandler<GetDvlaInfoEvent, Vehicle>
  {
    private readonly IVehicleReadRepository _readRepository;

    public GetDvlaInfoEventHandler(IVehicleReadRepository readRepository)
    {
      _readRepository = readRepository;
    }

    public async Task<Vehicle> Handle(GetDvlaInfoEvent request, CancellationToken cancellationToken)
    {
      if (request == null) throw new ArgumentNullException(nameof(request));
      if (string.IsNullOrEmpty(request.Registration)) throw new ArgumentNullException(nameof(request.Registration));

      return await _readRepository.Get(request.Registration);      
    }
  }
}
