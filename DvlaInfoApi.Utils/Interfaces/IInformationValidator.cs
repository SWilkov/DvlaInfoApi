using DvlaInfoApi.Utils.Validation.Models;

namespace DvlaInfoApi.Utils.Validation.Interfaces
{
  public interface IInformationValidator<T> where T : class
  {
    ValidationInformation Validate(T item);
  }
}
