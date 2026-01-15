using Crypty.ViewModels.Tools;
using System.Text.Json.Serialization;

namespace Crypty.Models.DataModels
{
    /// <summary>
    /// Represents raw (just after extraction) historical market data, including prices, market capitalizations, and total volumes over time
    /// </summary>
    public class HistoryRawData : ObservableObject
    {
        private List<List<double>> _prices = null!;
        [JsonPropertyName("prices")]
        public List<List<double>> Prices
        {
            get => _prices;
            set => SetProperty(ref _prices, value);
        }

        private List<List<double>> _marketCaps = null!;
        [JsonPropertyName("market_caps")]
        public List<List<double>> MarketCaps
        {
            get => _marketCaps;
            set => SetProperty(ref _marketCaps, value);
        }

        private List<List<double>> _totalVolumes = null!;
        [JsonPropertyName("total_volumes")]
        public List<List<double>> TotalVolumes
        {
            get => _totalVolumes;
            set => SetProperty(ref _totalVolumes, value);
        }
    }
}
