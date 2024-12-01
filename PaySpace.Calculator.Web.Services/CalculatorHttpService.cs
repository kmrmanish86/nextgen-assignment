using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Services
{
    public class CalculatorHttpService(IHttpClientFactory httpClientFactory, ILogger<CalculatorHttpService> logger) : ICalculatorHttpService
    {
        public async Task<List<PostalCode>> GetPostalCodesAsync()
        {
            logger.LogInformation("GetPostalCodesAsync Start");

            using (HttpClient httpClient = httpClientFactory.CreateClient("PaySpaceApiClient"))
            {
                var response = await httpClient.GetAsync("api/postalcode/postalcode");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Cannot fetch postal codes, status code: {response.StatusCode}");
                }

                logger.LogInformation("GetPostalCodesAsync End");

                return await response.Content.ReadFromJsonAsync<List<PostalCode>>() ?? [];
            }
        }

        public async Task<List<CalculatorHistory>> GetHistoryAsync()
        {
            logger.LogInformation("GetHistoryAsync Start");

            using (HttpClient httpClient = httpClientFactory.CreateClient("PaySpaceApiClient"))
            {
                var response = await httpClient.GetAsync("api/calculator/history");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Cannot fetch history, status code: {response.StatusCode}");
                }

                logger.LogInformation("GetHistoryAsync End");

                return await response.Content.ReadFromJsonAsync<List<CalculatorHistory>>() ?? [];
            }
        }

        public async Task<CalculateResult> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            logger.LogInformation($"CalculateTaxAsync Start | CalculationRequest: {JsonSerializer.Serialize(calculationRequest)}");

            using (HttpClient httpClient= httpClientFactory.CreateClient("PaySpaceApiClient"))
            {
                using StringContent jsonContent = new(
                    JsonSerializer.Serialize(calculationRequest),
                    Encoding.UTF8,
                    "application/json");

                var response = await httpClient.PostAsync("api/calculator/calculate-tax", jsonContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Cannot calculate tax, status code: {response.StatusCode}");
                }

                logger.LogInformation("CalculateTaxAsync End");

                return await response.Content.ReadFromJsonAsync<CalculateResult>();
            }
        }
    }
}