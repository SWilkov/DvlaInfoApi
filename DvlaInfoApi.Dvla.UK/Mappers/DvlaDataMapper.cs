using DvlaInfoApi.Dvla.UK.Interfaces;
using DvlaInfoApi.Dvla.UK.Models;
using DvlaInfoApi.Framework.Models;

namespace DvlaInfoApi.Dvla.UK.Mappers
{
  public class DvlaDataMapper : IDvlaInfoMapper
  {
    public DvlaInfo Map(DvlaGovDataModel source)
    {
      if (source == null) throw new ArgumentNullException("source");

      return new DvlaInfo
      {        
        TaxDueDate = source.TaxDueDate,
        TaxStatus = source.TaxStatus,
        Co2Emissions = source.Co2Emissions,
        DateOfLastV5CIssued = source.DateOfLastV5CIssued,
        MarkedForExport = source.MarkedForExport,
        TypeApproval = source.TypeApproval,
        Wheelplan = source.Wheelplan, 
      };
    }
  }
}
