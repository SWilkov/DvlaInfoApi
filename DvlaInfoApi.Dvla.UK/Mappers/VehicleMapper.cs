using DvlaInfoApi.Dvla.UK.Interfaces;
using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.Dvla.UK.Mappers
{
  public class VehicleMapper : IVehicleMapper
  {
    private readonly IDvlaInfoMapper _mapper;
    public VehicleMapper(IDvlaInfoMapper mapper) => _mapper = mapper;
    public Vehicle Map(Models.DvlaGovDataModel source)
    {
      if (source == null) throw new ArgumentNullException("source");

      return new Vehicle
      {
        DvlaInfo = _mapper.Map(source),
        EngineCapacity = source.EngineCapicity,
        Color = source.Colour,
        FuelType = source.FuelType,
        Make = source.Make,
        MonthRegistration = source.MonthOfFirstRegistration,
        MotExpiryDate = source.MotExpiryDate,
        MotStatus = source.MotStatus,
        Registration = source.RegistrationNumber
      };
    }
  }
}
