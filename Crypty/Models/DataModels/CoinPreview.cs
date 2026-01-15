using Crypty.ViewModels.Tools;
using System.Text.Json.Serialization;

namespace Crypty.Models.DataModels
{
    /// <summary>
    /// Represents a preview of a cryptocurrency coin, including basic identification and pricing information.
    /// </summary>
    public class CoinPreview : ObservableObject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        private string _iconUrl = null!;
        [JsonPropertyName("image")]
        public string IconUrl
        {
            get => _iconUrl;
            set => SetProperty(ref _iconUrl, value);
        }

        private decimal _price;
        [JsonPropertyName("current_price")]
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private DateTime _lastTimeUpdate;
        [JsonPropertyName("last_updated")]
        public DateTime LastUpdatedTime
        {
            get => _lastTimeUpdate;
            set => SetProperty(ref _lastTimeUpdate, value);
        }
    }
}
