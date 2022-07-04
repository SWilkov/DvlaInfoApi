﻿// <auto-generated />
using System;
using DvlaInfoApi.DataLayer.MySQL.EFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.Migrations
{
    [DbContext(typeof(DvlaDataContext))]
    [Migration("20220704091040_VehicleModifiedDate")]
    partial class VehicleModifiedDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels.DvlaInfoDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("Co2Emissions")
                        .HasColumnType("int")
                        .HasColumnName("co2_emissions");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("DateLastV5CIssued")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("date_last_v5c_issued");

                    b.Property<bool>("MarkedForExport")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("marked_for_export");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_at");

                    b.Property<DateTime>("TaxDueDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("tax_due_date");

                    b.Property<string>("TaxStatus")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("tax_status");

                    b.Property<string>("TypeApproval")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("type_approval");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int")
                        .HasColumnName("vehicle_id");

                    b.Property<string>("Wheelplan")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("wheelplan");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId")
                        .IsUnique();

                    b.ToTable("dvlaInfos");
                });

            modelBuilder.Entity("DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels.VehicleDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Color")
                        .HasColumnType("longtext")
                        .HasColumnName("colour");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<int>("EngineCapacity")
                        .HasColumnType("int")
                        .HasColumnName("engine_capacity");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("fuel_type");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("make");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("modified_at");

                    b.Property<string>("MonthRegistration")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("month_Registration");

                    b.Property<DateTime>("MotExpiryDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("mot_expiry_date");

                    b.Property<string>("MotStatus")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("mot_status");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("registration");

                    b.HasKey("Id");

                    b.HasIndex("Registration");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels.DvlaInfoDataModel", b =>
                {
                    b.HasOne("DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels.VehicleDataModel", "Vehicle")
                        .WithOne("DvlaInfo")
                        .HasForeignKey("DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels.DvlaInfoDataModel", "VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels.VehicleDataModel", b =>
                {
                    b.Navigation("DvlaInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
