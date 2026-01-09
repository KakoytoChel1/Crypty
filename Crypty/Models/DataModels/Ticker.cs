using Crypty.ViewModels.Tools;
using System.Text.Json.Serialization;

namespace Crypty.Models.DataModels
{
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

        private decimal _lastTradedPriceInTarget;
        [JsonPropertyName("last")]
        public decimal LastTradedPriceInTarget
        {
            get => _lastTradedPriceInTarget; 
            set => SetProperty(ref _lastTradedPriceInTarget, value);
        }

        private decimal _lastTradedVolumeInTarget;
        [JsonPropertyName("volume")]
        public decimal TotalVolumeInTarget
        {
            get => _lastTradedVolumeInTarget;
            set => SetProperty(ref _lastTradedVolumeInTarget, value);
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
