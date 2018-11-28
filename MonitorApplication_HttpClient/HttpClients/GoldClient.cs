using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace MonitorApplicationHttpClient
{
    /* HttpClient is a wrapper around HttpClient class. It is responsible for data retrival.   
     Currently uri is stored in appsettings.json  HttpClient is configured in Startup class.*/
    public class GoldClient
    {
        private readonly HttpClient _client;
        private readonly ILogger<GoldClient> _logger;

        // parts of URl necessary to retrive data
        private readonly string _actionCurrency = "dbXRates/{0}";
        private readonly string _usdCurrency = "USD";

        public GoldClient(HttpClient httpClient, ILoggerFactory loggerFactory)
        {
            _client = httpClient;
            _logger = loggerFactory.CreateLogger<GoldClient>();
        }

        public async Task<T> GetGoldValues<T>()
        {
            try
            {
                string action = string.Format(_actionCurrency, _usdCurrency);
                HttpResponseMessage response = await _client.GetAsync(action);
                T result = await response.Content.ReadAsAsync<T>();
                return result;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An HttpRequestException occured while to connecting to web API {ex.Message}");
                return default(T);
            }
            catch (Exception ex)
            {
                _logger.LogError($"A general error occured while connecting to web API {ex.Message}");
                return default(T);
            }
        }
    }
}
