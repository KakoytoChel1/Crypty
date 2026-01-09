using Crypty.ViewModels.Tools;
using System.Text.Json.Serialization;

namespace Crypty.Models.DataModels
{
    public class TickerMarket : ObservableObject
    {
        private string _marketName = null!;
        [JsonPropertyName("name")]
        public string MarketName
        {
            get => _marketName;
            set => SetProperty(ref _marketName, value);
        }
        private string _marketIdentifier = null!;
        [JsonPropertyName("identifier")]
        public string MarketIdentifier
        {
            get => _marketIdentifier;
            set => SetProperty(ref _marketIdentifier, value);
        }
    }
}
