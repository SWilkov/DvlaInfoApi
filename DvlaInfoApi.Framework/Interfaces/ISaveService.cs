using System.Threading.Tasks;

namespace DvlaInfoApi.Framework.Interfaces
{
  public interface ISaveService<T>
    where T: class
  {
    Task<T> Save(T item);
  }
}
