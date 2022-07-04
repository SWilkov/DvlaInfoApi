
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels
{
  public class VehicleDataModel : BaseDataModel
  {    
    [Column("make")]    
    public string Make { get; set; }        
    [Column("colour")]
    public string? Color { get; set; }
    [Column("registration")]
    [Required]
    public string Registration { get; set; }   
    [Column("fuel_type")]
    public string FuelType { get; set; }
    [Column("engine_capacity")]
    public int EngineCapacity { get; set; }
    [Column("month_Registration")]
    public string MonthRegistration { get; set; }
    [Column("mot_status")]
    public string MotStatus { get; set; }
    [Column("mot_expiry_date")]
    public DateTime MotExpiryDate { get; set; }
    public DvlaInfoDataModel DvlaInfo { get; set; }
    [Column("automatedVehicle")]
    public bool AutomatedVehicle { get; set; }
    [Column("revenueWeight")]
    public int RevenueWeight { get; set; }
    [Column("realDrivingEmissions")]
    public string RealDrivingEmissions { get; set; }
    [Column("euroStatus")]
    public string EuroStatus { get; set; }

  }
}
