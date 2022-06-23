using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DvlaInfoApi.DataLayer.Interfaces;
using DvlaInfoApi.DataLayer.MySQL.EFCore.Interfaces;
using DvlaInfoApi.Framework.Models;
using DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels;
using DvlaInfoApi.DataLayer.MySQL.EFCore.EFCore.Mappers;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddMySQLServer(this IServiceCollection services, string connectionString)
    {
      #region Database
      services.AddDbContext<Data.DvlaDataContext>(options =>
      {
        options.EnableSensitiveDataLogging(true);
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 32)),
        mySqlOptions =>
        {
          mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
      });
      #endregion           

      #region Repositories
      services.AddScoped<IRepository<Vehicle>, Repositories.VehicleRepository>();      
      services.AddScoped<IVehicleReadRepository, Repositories.VehicleReadRepository>();
      #endregion

      #region Mappers
      services.AddScoped<IVehicleMapper, Mappers.VehicleMapper>();
      services.AddScoped<IDvlaInfoMapper, DvlaInfoMapper>();
      #endregion
    }
  }
}
