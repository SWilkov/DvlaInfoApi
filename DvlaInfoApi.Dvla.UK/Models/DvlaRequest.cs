using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DvlaInfoApi.Dvla.UK.Models
{
  public class DvlaRequest
  {
    [JsonProperty("registrationNumber")]
    public string RegistrationNumber { get; set; }
  }
}
