using AW.Utilities.Commands.Interfaces;
using AW.Utilities.Validation.Interfaces;
using DvlaInfoApi.Dvla.UK.Models;
using DvlaInfoApi.Dvla.UK.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DvlaInfoApi.Dvla.UK.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddDvlaGovData(this IServiceCollection services, DvlaSettings settings)
    {
      #region Http Clients
      services.AddHttpClient<DvlaGovService>(s =>
      {
        s.BaseAddress = new Uri(settings.BaseUrl ?? throw new ArgumentException("Invalid Dvla base url"));
        s.DefaultRequestHeaders.Add("x-api-key", settings.ApiKey ?? throw new ArgumentException("Invalid dvla api key"));
      });
      #endregion

      #region Mappers
      services.AddScoped<Interfaces.IDvlaInfoMapper, Mappers.DvlaDataMapper>();
      services.AddScoped<Interfaces.IVehicleMapper, Mappers.VehicleMapper>();
      #endregion

      #region Commands
      services.AddScoped<ICommandHandlerAsync<Commands.VehicleEnquiryCommand>, Commands.VehicleEnquiryHandler>();
      #endregion

      
    }
  }
}
