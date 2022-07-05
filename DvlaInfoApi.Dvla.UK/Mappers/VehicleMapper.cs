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
        Color = string.IsNullOrWhiteSpace(source.Colour) ? string.Empty : source.Colour,
        FuelType = string.IsNullOrWhiteSpace(source.FuelType) ? string.Empty : source.FuelType,
        Make = string.IsNullOrWhiteSpace(source.Make) ? string.Empty : source.Make,
        MonthRegistration = source.MonthOfFirstRegistration,
        MotExpiryDate = source.MotExpiryDate,
        MotStatus = string.IsNullOrWhiteSpace(source.MotStatus) ? "" : source.MotStatus,
        Registration = source.RegistrationNumber,
        AutomatedVehicle = source.AutomatedVehicle,
        RealDrivingEmissions = string.IsNullOrWhiteSpace(source.RealDrivingEmissions) ? string.Empty : source.RealDrivingEmissions,
        EuroStatus = string.IsNullOrWhiteSpace(source.EuroStatus) ? string.Empty : source.EuroStatus,
        RevenueWeight = source.RevenueWeight
      };
    }
  }
}
