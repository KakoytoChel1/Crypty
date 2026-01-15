using System.Text.Json.Serialization;

namespace Crypty.Models.DataModels
{
    /// <summary>
    /// Represents a set of image URLs for a coin, providing access to thumbnail, small, and large image variants
    /// </summary>
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
