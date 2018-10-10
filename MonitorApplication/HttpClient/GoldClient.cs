﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace MonitorApplication.HttpClient
{
    using HttpClient = System.Net.Http.HttpClient;

    public class GoldClient
    {
        private readonly HttpClient _client;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<GoldClient> _logger;
        private readonly string _actionCurrency = "dbXRates/{0}";
        private readonly string _usdCurrency = "USD";


        public GoldClient(HttpClient httpClient, ILoggerFactory loggerFactory)
        {
            _client = httpClient;
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger<GoldClient>();
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
                _logger.LogError($"An error occured connecting to gold API {ex}");
                return default(T);
            }
        }
    }
}