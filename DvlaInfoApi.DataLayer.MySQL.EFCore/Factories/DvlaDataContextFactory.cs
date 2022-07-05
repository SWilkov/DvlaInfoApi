using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DvlaInfoApi.DataLayer.MySQL.EFCore.Data;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Factories
{
  public class DvlaDataContextFactory : IDesignTimeDbContextFactory<DvlaDataContext>
  {
    public DvlaDataContext CreateDbContext(string[] args)
    {
      var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONN_STR");

      var optionsBuilder = new DbContextOptionsBuilder<DvlaDataContext>();
      optionsBuilder.UseMySql("Server=wilkos-mysql.mysql.database.azure.com; Port=3306; Database=dvladata; Uid=wilkov@wilkos-mysql; Pwd=Gr33nbay; SslMode=Preferred;",
        new MySqlServerVersion(new Version(5, 7, 32)),
        options =>
        {
          options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });

      return new DvlaDataContext(optionsBuilder.Options);
    }
  }
}
