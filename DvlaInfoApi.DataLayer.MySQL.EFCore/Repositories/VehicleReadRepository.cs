using Microsoft.EntityFrameworkCore;
using DvlaInfoApi.DataLayer.Interfaces;
using DvlaInfoApi.DataLayer.MySQL.EFCore.Data;
using DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels;
using DvlaInfoApi.DataLayer.MySQL.EFCore.Interfaces;
using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Repositories
{
  public class VehicleReadRepository : IVehicleReadRepository
  {
    private readonly IVehicleMapper _vehicleMapper;
    private readonly DvlaDataContext _context;
    public VehicleReadRepository(IVehicleMapper mapper,
      DvlaDataContext context)
    {
      _context = context;
      _vehicleMapper = mapper;
    }

    public async Task<Vehicle> Get(string registration)
    {
      if (string.IsNullOrEmpty(registration))
        throw new ArgumentNullException(nameof(registration));

      var vehicle = await _context.Vehicles.Include(x => x.DvlaInfo)
                                           .FirstOrDefaultAsync(x => x.Registration.ToLower() == registration.ToLower());

      if (vehicle == null)
        return null;

      return _vehicleMapper.Map(vehicle);
    }
    
    public async Task<bool> Exists(string registration)
    {
      if (string.IsNullOrWhiteSpace(registration)) throw new ArgumentNullException("No registration params!");
      var formattedRegistration = registration.Trim().ToLower();

      var exists = await _context.Vehicles.FirstOrDefaultAsync(x => x.Registration.ToLower() == formattedRegistration);
      return exists != null && exists.Id > 0;
    }
  }
}
