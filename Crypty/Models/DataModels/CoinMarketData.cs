using Crypty.ViewModels.Tools;
using System.Text.Json.Serialization;

namespace Crypty.Models.DataModels
{
    /// <summary>
    /// Represents market data for a cryptocurrency, including current prices, price change percentages, and total
    /// trading volume across multiple currencies
    /// </summary>
    public class CoinMarketData : ObservableObject
    {
        private Dictionary<string, decimal> _currentPriceData = null!;
        [JsonPropertyName("current_price")]
        public Dictionary<string, decimal> CurrentPriceData
        {
            get => _currentPriceData;
            set => SetProperty(ref _currentPriceData, value);
        }

        private double _priceChangePercentage24h;
        [JsonPropertyName("price_change_percentage_24h")]
        public double PriceChangePercentage24h
        {
            get => _priceChangePercentage24h;
            set => SetProperty(ref _priceChangePercentage24h, value);
        }

        private double _priceChangePercentage7d;
        [JsonPropertyName("price_change_percentage_7d")]
        public double PriceChangePercentage7d
        {
            get => _priceChangePercentage7d;
            set => SetProperty(ref _priceChangePercentage7d, value);
        }

        private Dictionary<string, decimal> _totalVolumeData = null!;
        [JsonPropertyName("total_volume")]
        public Dictionary<string, decimal> TotalVolumeData
        {
            get => _totalVolumeData;
            set => SetProperty(ref _totalVolumeData, value);
        }
    }
}
