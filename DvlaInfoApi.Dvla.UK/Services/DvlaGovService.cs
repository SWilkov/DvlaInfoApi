using DvlaInfoApi.Dvla.UK.Interfaces;
using DvlaInfoApi.Dvla.UK.Models;
using DvlaInfoApi.Framework.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace DvlaInfoApi.Dvla.UK.Services
{
  public class DvlaGovService
  {
    private readonly HttpClient _client;
    private readonly IVehicleMapper _mapper; 
    private readonly ILogger _logger;
    public DvlaGovService(HttpClient client,
       IVehicleMapper mapper,
       ILogger<DvlaGovService> logger)
    {
      _client = client;
      _mapper = mapper;
      _logger = logger;
    }

    public async Task<Vehicle> Get(DvlaRequest request)
    {
      if (request == null)
        throw new ArgumentNullException(nameof(request));
      if (string.IsNullOrWhiteSpace(request.RegistrationNumber))
        throw new ArgumentNullException("No Registration number detected!");
            
      //Serialize request data!
      var serialized = JsonConvert.SerializeObject(request, Formatting.Indented);
      var content = new StringContent(serialized, Encoding.UTF8, "application/json");

      var response = await _client.PostAsync("", content);
      if (!response.IsSuccessStatusCode)
      {
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
          return null;

        //log as DVLA Gov is sending other failures        
        _logger.LogInformation($"Something has gone wrong with DvlaGovService: {response.RequestMessage}");
      }

      //response.EnsureSuccessStatusCode();

      var body = await response.Content.ReadAsStringAsync();
      if (string.IsNullOrWhiteSpace(body))
      {
        //TODO log
        throw new ArgumentException($"Somethings gone wrong getting the Vehicle from the Gov api for input: {request.RegistrationNumber}");
      }

      var dvlaModel = JsonConvert.DeserializeObject<DvlaGovDataModel>(body);

      if (dvlaModel == null || string.IsNullOrEmpty(dvlaModel.RegistrationNumber))
      {
        _logger.LogInformation($"no gov data for input : ${request.RegistrationNumber}");
        return null;
      }

      return _mapper.Map(dvlaModel);
    }
  }
}
