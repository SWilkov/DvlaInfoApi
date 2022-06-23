using DvlaInfoApi.DataLayer.Interfaces;
using DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels;
using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Interfaces
{
  public interface IDvlaInfoMapper : IMapper<DvlaInfo, DvlaInfoDataModel>
  {
    DvlaInfoDataModel Map(DvlaInfo source);
  }
}
