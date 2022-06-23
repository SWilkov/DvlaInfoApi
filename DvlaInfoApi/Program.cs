using DvlaInfoApi.DataLayer.MySQL.EFCore.Extensions;
using DvlaInfoApi.Dvla.UK.Extensions;
using DvlaInfoApi.Dvla.UK.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Text.Json;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
      services.Configure<JsonSerializerOptions>(options =>
      {
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

      });
      #region database
      services.AddMySQLServer(Environment.GetEnvironmentVariable("DVLA_MYSQL_CONN_STR") ?? throw new ArgumentException("Could not retrieve MySQL connection string!"));
      #endregion

      services.AddMediatR(Assembly.GetExecutingAssembly());

      services.AddDvlaGovData(new DvlaSettings(
        Environment.GetEnvironmentVariable("DVLA_BASE_URL") ?? throw new ArgumentException("Error retrieving DVLA url!"),
        Environment.GetEnvironmentVariable("DVLA_API_KEY") ?? throw new ArgumentException("Error retrieving DVLA Api Key!")));
    })
    .Build();

host.Run();