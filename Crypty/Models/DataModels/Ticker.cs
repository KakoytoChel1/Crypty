using Crypty.ViewModels.Tools;

namespace Crypty.Models.DataModels
{
    public class Ticker : ObservableObject
    {
        public string BaseCoin { get; set; } = null!;
        public string TargetCurrency { get; set; } = null!;
        public string MarketName { get; set; } = null!;
        public string MarketIdentifier { get; set; } = null!;
        public decimal LastTradedPrice { get; set; }
        public decimal TotalVolume { get; set; }
        public string TradePlatformUrl { get; set; } = null!;
    }
}
