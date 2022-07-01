using DvlaInfoApi.DataLayer.Interfaces;
using DvlaInfoApi.Framework.Models;
using MediatR;

namespace DvlaInfoApi.Queries
{
  public class GetAllDvlaInfoHandler : IRequestHandler<GetAllDvlaInfoEvent, IEnumerable<Vehicle>>
  {
    private readonly IVehicleReadRepository _repository;

    public GetAllDvlaInfoHandler(IVehicleReadRepository repository)
    {
      _repository = repository;
    }
    public async Task<IEnumerable<Vehicle>> Handle(GetAllDvlaInfoEvent request, CancellationToken cancellationToken)
    {
      if (request == null) throw new ArgumentNullException(nameof(request));

      return await _repository.GetAll(true);
    }
  }

  public class GetAllDvlaInfoEvent : IRequest<IEnumerable<Vehicle>>
  {

  }
}
