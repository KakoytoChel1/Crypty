using Crypty.Models.DataModels;
using Crypty.Services.IServices;
using System.Net.Http;
using System.Net.Http.Json;

namespace Crypty.Services
{
    public class CoinDataProviderService : ICoinDataProviderService
    {
        private readonly IConfigurationService _configurationService;
        private readonly HttpClient _httpClient;

        private string _targetCurrency;

        public CoinDataProviderService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;

            // Setting up our coin data provider service url
            string? providerUrl = _configurationService.Get<string>("provider_url");

            if(providerUrl == null)
                throw new ArgumentNullException("Provider URL is not configured.");

            _httpClient = new HttpClient() { BaseAddress = new Uri(providerUrl) };

            // Avoiding 403 errors by setting up user agent header
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CryptyApp/1.0");

            // Configuring api key like header
            string? apiKey = _configurationService.Get<string>("api_key");
            _httpClient.DefaultRequestHeaders.Add("x-cg-demo-api-key", apiKey);

            // Configuring target currency
            _targetCurrency = _configurationService.Get<string>("target_currency") ?? "usd";
        }

        public async Task<List<CoinPreview>?> GetTopPopularCoinPreviewsAsync(int number)
        {
            List<CoinPreview>? result;

            result = await _httpClient.GetFromJsonAsync<List<CoinPreview>>($"coins/markets?vs_currency={_targetCurrency}&order=market_cap_desc&per_page={number}");

            return result;
        }

        public async Task<CoinDetails?> GetCoinDataById(string coinId)
        {
            CoinDetails? result;

            result = await _httpClient.GetFromJsonAsync<CoinDetails>($"coins/{coinId}?localization=false&tickers=true&market_data=true&community_data=false&developer_data=false");

            return result;
        }

        public string GetCoinHistory(string coinId, int days)
        {
            return string.Empty;
        }
    }
}
