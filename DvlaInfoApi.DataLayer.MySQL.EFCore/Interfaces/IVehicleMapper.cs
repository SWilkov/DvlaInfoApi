using DvlaInfoApi.DataLayer.Interfaces;
using DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels;
using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Interfaces
{
  public interface IVehicleMapper :
    IMapper<VehicleDataModel, Vehicle>,
    IMapper<Vehicle, VehicleDataModel>
  {

  }
}
