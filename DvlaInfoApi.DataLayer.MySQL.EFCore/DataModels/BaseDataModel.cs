
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DvlaInfoApi.DataLayer.MySQL.EFCore.DataModels
{
  public class BaseDataModel
  {
    [Column("id")]
    public int Id { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("modified_at")]
    public DateTime ModifiedAt { get; set; }
  }
}
