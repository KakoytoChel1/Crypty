using Crypty.ViewModels.Tools;
using System.Text.Json.Serialization;

namespace Crypty.Models.DataModels
{
    /// <summary>
    /// Represents ticker (market) information for a cryptocurrency trading pair, including pricing, volume, and market
    /// details
    /// </summary>
    public class Ticker : ObservableObject
    {
        private string _baseCoin = null!;
        [JsonPropertyName("base")]
        public string BaseCoin
        {
            get => _baseCoin;
            set => SetProperty(ref _baseCoin, value);
        }

        private string _targetCurrency = null!;
        [JsonPropertyName("target")]
        public string TargetCurrency
        {
            get => _targetCurrency;
            set => SetProperty(ref _targetCurrency, value);
        }
        private TickerMarket _market = null!;
        [JsonPropertyName("market")]
        public TickerMarket Market
        {
            get => _market;
            set => SetProperty(ref _market, value);
        }

        private Dictionary<string, decimal> _lastTradedPriceData = null!;
        [JsonPropertyName("converted_last")]
        public Dictionary<string, decimal> LastTradedPriceData
        {
            get => _lastTradedPriceData; 
            set => SetProperty(ref _lastTradedPriceData, value);
        }

        private Dictionary<string, decimal> _lastTradedVolumeData = null!;
        [JsonPropertyName("converted_volume")]
        public Dictionary<string, decimal> TotalVolumeData
        {
            get => _lastTradedVolumeData;
            set => SetProperty(ref _lastTradedVolumeData, value);
        }

        private string _tradePlatformUrl = null!;
        [JsonPropertyName("trade_url")]
        public string TradePlatformUrl
        {
            get => _tradePlatformUrl;
            set => SetProperty(ref _tradePlatformUrl, value);
        }
    }
}
