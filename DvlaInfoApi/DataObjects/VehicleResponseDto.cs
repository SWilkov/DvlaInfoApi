using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.DataObjects
{
  public class VehicleResponseDto 
  {
    public string Status { get; private set; }
    public Vehicle Data { get; private set; }
    public VehicleResponseDto(string status, Vehicle data)
    {
      this.Data = data;
      this.Status = status;
    }
  }
}
