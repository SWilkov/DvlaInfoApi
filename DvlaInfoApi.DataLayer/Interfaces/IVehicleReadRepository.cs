using System.Threading.Tasks;
using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.DataLayer.Interfaces
{
  public interface IVehicleReadRepository
  {
    Task<Vehicle> Get(string registration);
    Task<bool> Exists(string registration);
  }
}