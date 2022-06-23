namespace DvlaInfoApi.Framework.Models
{
  public class Vehicle : Base
  {
    public string Registration { get; set; }
    public string Make { get; set; }
    public string Color { get; set; }
    public string FuelType { get; set; }
    public int EngineCapacity { get; set; }
    public string MonthRegistration { get; set; }
    public string MotStatus { get; set; }
    public DateTime MotExpiryDate { get; set; }
    public DvlaInfo DvlaInfo { get; set; }
  }
}
