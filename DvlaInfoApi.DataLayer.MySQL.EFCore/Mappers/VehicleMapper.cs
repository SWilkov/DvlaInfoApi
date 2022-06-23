using DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels;
using DvlaInfoApi.DataLayer.MySQL.EFCore.Interfaces;
using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Mappers
{
  public class VehicleMapper : IVehicleMapper
  {
    private readonly IDvlaInfoMapper _mapper;

    public VehicleMapper(IDvlaInfoMapper mapper)
    {
      _mapper = mapper;
    }

    public VehicleDataModel Map(Vehicle source)
    {
      if (source == null)
        return null;

      var dm = new VehicleDataModel
      {
        Id = source.Id,
        FuelType = source.FuelType,
        Make = source.Make,
        Registration = source.Registration,
        EngineCapacity = source.EngineCapacity,
        MonthRegistration = source.MonthRegistration,
        Color = source.Color,
        MotExpiryDate = source.MotExpiryDate,
        MotStatus = source.MotStatus,
        DvlaInfo = _mapper.Map(source.DvlaInfo)
      };

      return dm;
    }

    public Vehicle Map(VehicleDataModel source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      return new Vehicle
      {
        Id = source.Id,
        Color = source.Color,
        FuelType = source.FuelType,
        Make = source.Make,
        Registration = source.Registration,
        DvlaInfo = _mapper.Map(source.DvlaInfo)
      };
    }
  }
}
