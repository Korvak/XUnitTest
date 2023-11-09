//Default
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
//Other
using FinhubEntities.Models;
using FinhubServiceContracts.Contracts;
using FinhubServiceContracts.DTO;
using FinhubServices.ConfigOptions;
using System.ComponentModel.DataAnnotations;
using EntitiesServices;

namespace FinhubServices.Services
{
    public class FinhubService : IFinhubService
    {
        private readonly FinhubOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public FinhubService(IHttpClientFactory httpClientFactory, IOptions<FinhubOptions> options) 
        {
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
        }

        protected async Task<string> ReadResponseAsString(HttpResponseMessage response)
        {
            Stream stream = await response.Content.ReadAsStreamAsync();
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public async Task<FinhubCompanyProfileResponse?> GetCompanyProfile(FinhubCompanyProfileRequest request)
        {
            ModelValidator.ValidateModel(request); //we validate it and launches error if invalid.
            if (string.IsNullOrWhiteSpace(request.StockSymbol) ) { throw new ArgumentNullException(nameof(request.StockSymbol)); }
            using (var client = _httpClientFactory.CreateClient() ) //since we cannot use the IHttpClient factory we do it like this
            {
                string url = $"{_options.Weburl}{_options.CompanyProfileUrl}?symbol={request.StockSymbol}&token={_options.ApiKey}";
                HttpRequestMessage requestMsg = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };
                HttpResponseMessage response = await client.SendAsync(requestMsg);
                string jsonData = await ReadResponseAsString(response);
                FinhubCompanyProfileModel? companyModel = JsonSerializer.Deserialize<FinhubCompanyProfileModel>(jsonData);
                return companyModel?.ToCompanyProfileResponse();
            }
        }

        public async Task<FinhubQuoteResponse?> GetStockPriceQuote(FinhubQuoteRequest request)
        {
            ModelValidator.ValidateModel(request); //we validate it and launches error if invalid.
            if (string.IsNullOrWhiteSpace(request.StockSymbol)) { throw new ArgumentNullException(nameof(request.StockSymbol)); }
            using (var client = _httpClientFactory.CreateClient() ) //since we cannot use the IHttpClient factory we do it like this
            {
                HttpRequestMessage requestMsg = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{_options.Weburl}{_options.QuoteUrl}?symbol={request.StockSymbol}&token={_options.ApiKey}"),
                    Method = HttpMethod.Get
                };
                HttpResponseMessage response = await client.SendAsync(requestMsg);
                string jsonData = await ReadResponseAsString(response);
                FinhubQuoteModel? companyModel = JsonSerializer.Deserialize<FinhubQuoteModel>(jsonData);
                return companyModel?.ToQuoteResponse();
            }
        }
    }
}
