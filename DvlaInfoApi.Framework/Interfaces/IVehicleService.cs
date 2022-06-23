using DvlaInfoApi.Framework.Enums;
using DvlaInfoApi.Framework.Models;
using System.Threading.Tasks;

namespace DvlaInfoApi.Framework.Interfaces
{
  public interface IVehicleService
  {
    Task<Vehicle> Get(string input, SearchType searchType = SearchType.Registration);
    Task<Vehicle> Save(Vehicle vehicle);
    bool IsNewCar(Vehicle vehicle);
    //Task DeleteTests(Vehicle vehicle);
  }
}
