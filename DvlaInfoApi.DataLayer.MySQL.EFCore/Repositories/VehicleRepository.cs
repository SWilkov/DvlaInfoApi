using DvlaInfoApi.DataLayer.Interfaces;
using DvlaInfoApi.DataLayer.MySQL.EFCore.Data;
using DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels;
using DvlaInfoApi.DataLayer.MySQL.EFCore.Interfaces;
using DvlaInfoApi.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Repositories
{
  public class VehicleRepository : IRepository<Vehicle>
  {
    private readonly IVehicleMapper _vehicleMapper;
    private readonly DvlaDataContext _context;
    private readonly IVehicleReadRepository _vehicleReadRepository;
    public VehicleRepository(IVehicleMapper vehicleMapper,
      DvlaDataContext context, IVehicleReadRepository vehicleReadRepository)
    {
      _vehicleMapper = vehicleMapper;
      _context = context;
      _vehicleReadRepository = vehicleReadRepository;
    }

    public Task Delete(Vehicle item)
    {
      throw new NotImplementedException();
    }

    public async Task<Vehicle> Save(Vehicle vehicle)
    {
      if (vehicle == null)
        throw new ArgumentNullException(nameof(vehicle));

      var dm = _vehicleMapper.Map(vehicle);
      var exists = await _vehicleReadRepository.Exists(vehicle.Registration);
      if (vehicle.Id == default(int) || !exists)
      {
        dm.CreatedAt = DateTime.Now;
        var entity = await _context.Vehicles.AddAsync(dm);
        if (entity.State != Microsoft.EntityFrameworkCore.EntityState.Added)
        {
          //TODO log
        }

        await _context.SaveChangesAsync();
        vehicle.Id = entity.Entity.Id;

        return vehicle;
      }
      else
      {
        var existing = _context.Vehicles.FirstOrDefault(x => x.Registration == vehicle.Registration);
        if (existing is not null)
        {
          _context.Entry(existing).CurrentValues.SetValues(dm);
          var rows = await _context.SaveChangesAsync();

          if (rows == default(int))
          {
            //TODO log
          }

          return _vehicleMapper.Map(dm);
        }
        else
          return vehicle;
      }
    } 
  }
}
