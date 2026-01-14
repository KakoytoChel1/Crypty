using Crypty.ViewModels.Tools;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Crypty.Models.DataModels
{
    public class CoinDetails : ObservableObject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        private Dictionary<string, string> _descriptionData = null!;
        [JsonPropertyName("description")]
        public Dictionary<string, string> DescriptionData
        {
            get => _descriptionData;
            set => SetProperty(ref _descriptionData, value);
        }

        private CoinImage _coinImage = null!;
        [JsonPropertyName("image")]
        public CoinImage CoinImage
        {
            get => _coinImage;
            set => SetProperty(ref _coinImage, value);
        }

        private CoinMarketData _marketData = null!;
        [JsonPropertyName("market_data")]
        public CoinMarketData MarketData
        {
            get => _marketData;
            set => SetProperty(ref _marketData, value);
        }

        private ObservableCollection<Ticker> _tickers = null!;
        [JsonPropertyName("tickers")]
        public ObservableCollection<Ticker> Tickers
        {
            get => _tickers;
            set => SetProperty(ref _tickers, value);
        }

        private ObservableCollection<HistoryPoint> _coinHistoryPer24h = null!;
        [JsonIgnore]
        public ObservableCollection<HistoryPoint> CoinHistoryPer24h
        {
            get => _coinHistoryPer24h;
            set => SetProperty(ref _coinHistoryPer24h, value);
        }

        private ObservableCollection<HistoryPoint> _coinHistoryPer7d = null!;
        [JsonIgnore]
        public ObservableCollection<HistoryPoint> CoinHistoryPer7d
        {
            get => _coinHistoryPer7d;
            set => SetProperty(ref _coinHistoryPer7d, value);
        }
    }
}
