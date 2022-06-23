namespace DvlaInfoApi.Dvla.UK.Models
{
  public class DvlaSettings
  {
    public string BaseUrl { get; private set; }
    public string ApiKey { get; private set; }
    public DvlaSettings(string baseUrl, string apiKey)
    {
      this.BaseUrl = baseUrl;
      this.ApiKey = apiKey;
    }
  }
}
