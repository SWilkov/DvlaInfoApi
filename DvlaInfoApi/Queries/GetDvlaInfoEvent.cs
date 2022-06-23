using DvlaInfoApi.Framework.Models;
using MediatR;

namespace DvlaInfoApi.Queries
{
  public class GetDvlaInfoEvent : IRequest<Vehicle>
  {

    public string Registration { get; private set; }
    public GetDvlaInfoEvent(string registration) => Registration = registration;
  }
}
