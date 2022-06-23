using DvlaInfoApi.DataLayer.Interfaces;
using DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels;
using DvlaInfoApi.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.EFCore.Mappers
{
  public class DvlaInfoMapper : Interfaces.IDvlaInfoMapper
  {
    public DvlaInfo Map(DvlaInfoDataModel source)
    {
      if (source == null) throw new ArgumentNullException(nameof(source));

      return new DvlaInfo
      {
        Id = source.Id,
        Co2Emissions = source.Co2Emissions,
        MarkedForExport = source.MarkedForExport,
        DateOfLastV5CIssued = source.DateLastV5CIssued,
        TaxDueDate = source.TaxDueDate,
        TaxStatus = source.TaxStatus,
        TypeApproval = source.TypeApproval,
        Wheelplan = source.Wheelplan
      };
    }

    public DvlaInfoDataModel Map(DvlaInfo source)
    {
      return new DvlaInfoDataModel
      {
        Id = source.Id,
        Co2Emissions = source.Co2Emissions,
        MarkedForExport = source.MarkedForExport,
        DateLastV5CIssued = source.DateOfLastV5CIssued,
        TaxDueDate = source.TaxDueDate,
        TaxStatus = source.TaxStatus,
        TypeApproval = source.TypeApproval,
        Wheelplan = source.Wheelplan
      };
    }
  }
}
