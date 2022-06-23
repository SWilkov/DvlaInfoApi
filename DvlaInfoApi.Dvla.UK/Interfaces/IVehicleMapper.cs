using DvlaInfoApi.Dvla.UK.Models;
using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.Dvla.UK.Interfaces
{
  public interface IVehicleMapper
  {
    Vehicle Map(DvlaGovDataModel source);
  }
}