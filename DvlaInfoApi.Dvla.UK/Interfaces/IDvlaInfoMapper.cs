using DvlaInfoApi.Dvla.UK.Models;
using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.Dvla.UK.Interfaces
{
  public interface IDvlaInfoMapper
  {
    DvlaInfo Map(DvlaGovDataModel source);
  }
}