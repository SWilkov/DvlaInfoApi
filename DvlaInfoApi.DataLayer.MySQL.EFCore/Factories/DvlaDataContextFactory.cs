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
      optionsBuilder.UseMySql("Server=localhost; Port=3307; Uid=root; Pwd=secret; database=dvladata;",
        new MySqlServerVersion(new Version(5, 7, 32)),
        options =>
        {
          options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });

      return new DvlaDataContext(optionsBuilder.Options);
    }
  }
}
