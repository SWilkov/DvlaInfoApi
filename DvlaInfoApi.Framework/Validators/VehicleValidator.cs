using DvlaInfoApi.Framework.Models;
using DvlaInfoApi.Utils.Validation.Enums;
using DvlaInfoApi.Utils.Validation.Interfaces;
using DvlaInfoApi.Utils.Validation.Models;

namespace DvlaInfoApi.Framework.Validators
{
  public class VehicleValidator : IInformationValidator<Vehicle>
  {
    public ValidationInformation Validate(Vehicle item)
    {
      if (item is null) throw new ArgumentNullException(nameof(item));
      var info = new ValidationInformation("", Result.Valid);

      if (string.IsNullOrWhiteSpace(item.Registration))
      {
        info.Result = Result.Invalid;
        info.Message = "Missing Registration number! ";
        return info;
      }

      if (item.DvlaInfo is null)
      {
        info.Result = Result.Invalid;
        info.Message = "Missing DvlaInfo details! ";
        return info;
      }

      if (string.IsNullOrWhiteSpace(item.DvlaInfo.TypeApproval) && string.IsNullOrWhiteSpace(item.DvlaInfo.TaxStatus)
        && string.IsNullOrWhiteSpace(item.MotStatus))
      {
        info.Result = Result.Invalid;
        info.Message = "Missing TypeApproval, Tax & MOT status";
      }

      return info;
    }
  }
}
