using DvlaInfoApi.Framework.Models;
using DvlaInfoApi.Utils.Validation.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DvlaInfoApi.Framework.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddDvlaFrameworkExtras(this IServiceCollection services)
    {
      #region Validators
      services.AddScoped<IInformationValidator<Vehicle>, Validators.VehicleValidator>();
      #endregion
    }
  }
}
