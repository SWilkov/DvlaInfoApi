
namespace DvlaInfoApi.Framework.Models
{
  public class DvlaInfo : Base
  {
    public int Co2Emissions { get; set; }
    public bool MarkedForExport { get; set; }
    public string TypeApproval { get; set; }
    public DateTime TaxDueDate { get; set; }
    public string TaxStatus { get; set; }
    public DateTime DateOfLastV5CIssued { get; set; }
    public string Wheelplan { get; set; }
  }
}
