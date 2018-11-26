using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Whoops.Services
{
    public class GeoCoordServices
    {
        private readonly ILogger _logger;
        private readonly IConfigurationRoot _config;

        public GeoCoordServices(ILogger<GeoCoordServices> logger, IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<GeoCoordResults> GetCoords(string name)
        {
            var result = new GeoCoordResults
            {
                Result = false,
                Message = "Failed to retrieve coordinates"
            };

            try
            {
                var locationName = WebUtility.UrlEncode(name);
                var bingKey = _config["BingApiKey"];

                var locationUrl = $"http://dev.virtualearth.net/REST/v1/locations?q={locationName}&key={bingKey}";

                var client = new HttpClient();
                var json = await client.GetStringAsync(locationUrl);
                var bingResponse  = JsonConvert.DeserializeObject<BingLocationResponse>(json);

                var coords = bingResponse.resourceSets.FirstOrDefault()?
                                                      .resources
                                                      .FirstOrDefault(r=>r.confidence.Equals("high",StringComparison.OrdinalIgnoreCase))?
                                                      .geocodePoints
                                                      .FirstOrDefault()?.coordinates;

                if (coords == null)
                    result.Message = $"Can't find confident coordinates for location: {name}";

                else
                {
                    result.Latitude = coords.FirstOrDefault();
                    result.Longtitude = coords.ElementAtOrDefault(1);
                    result.Result = true;
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {

            }


            return result;
        }
    }
}
