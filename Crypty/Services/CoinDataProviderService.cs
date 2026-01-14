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

        private string _targetCurrency = "usd";

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
        }

        public async Task<List<CoinPreview>?> GetTopPopularCoinPreviewsAsync(int number)
        {
            List<CoinPreview>? result;

            result = await _httpClient.GetFromJsonAsync<List<CoinPreview>>($"coins/markets?vs_currency={_targetCurrency}&order=market_cap_desc&per_page={number}");

            return result;
        }

        public async Task<CoinDetails?> GetCoinDataByIdAsync(string coinId)
        {
            CoinDetails? result;

            result = await _httpClient.GetFromJsonAsync<CoinDetails>($"coins/{coinId}?localization=false&tickers=true&market_data=true&community_data=false&developer_data=false");

            return result;
        }

        public async Task<List<HistoryPoint>?> GetCoinHistory(string coinId, int days)
        {
            List<HistoryPoint>? result = null;

            HistoryRawData? rawDatarawData = await _httpClient.GetFromJsonAsync<HistoryRawData>($"coins/{coinId}/market_chart?vs_currency={_targetCurrency}&days={days}");

            if(rawDatarawData != null)
            {
                foreach(var pricePoint in rawDatarawData.Prices)
                {
                    DateTime time = DateTimeOffset.FromUnixTimeMilliseconds((long)pricePoint[0]).DateTime;
                    decimal price = (decimal)pricePoint[1];
                    result ??= new List<HistoryPoint>();
                    result.Add(new HistoryPoint() { Time = time, Price = price });
                }

                return result;
            }

            return result;
        }
    }
}
