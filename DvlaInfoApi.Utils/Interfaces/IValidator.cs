using DvlaInfoApi.Utils.Validation.Enums;

namespace DvlaInfoApi.Utils.Validation.Interfaces
{
  public interface IValidator<T> where T : class
  {
    Result Validate(T item);
  }
}
