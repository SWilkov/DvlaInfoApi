using Microsoft.EntityFrameworkCore;
using DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Data
{
  public class DvlaDataContext : DbContext
  {
    public DbSet<VehicleDataModel> Vehicles { get; set; }
    public DbSet<DvlaInfoDataModel> dvlaInfos { get; set; }
    public DvlaDataContext(DbContextOptions<DvlaDataContext> options): base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<DvlaInfoDataModel>(entity =>
      {
        entity.HasKey(x => x.Id);
        entity.Property(x => x.TaxStatus).IsRequired();
      });
            
      modelBuilder.Entity<VehicleDataModel>(entity =>
      {
        entity.HasKey(x => x.Id);
        entity.HasOne(x => x.DvlaInfo)
              .WithOne(x => x.Vehicle)
              .HasForeignKey<DvlaInfoDataModel>(x => x.VehicleId)
              .OnDelete(DeleteBehavior.Cascade);
        entity.HasIndex(x => x.Registration);
        entity.Property(x => x.Registration).IsRequired();
      });

      base.OnModelCreating(modelBuilder);
    }
  }
}
