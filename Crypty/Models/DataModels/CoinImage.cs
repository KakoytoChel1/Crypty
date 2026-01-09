using System.Text.Json.Serialization;

namespace Crypty.Models.DataModels
{
    public class CoinImage
    {
        [JsonPropertyName("thumb")]
        public string? Thumb { get; set; }

        [JsonPropertyName("small")]
        public string? Small { get; set; }

        [JsonPropertyName("large")]
        public string? Large { get; set; }
    }
}
