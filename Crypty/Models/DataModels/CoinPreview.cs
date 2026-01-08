using Crypty.ViewModels.Tools;

namespace Crypty.Models.DataModels
{
    public class CoinPreview : ObservableObject
    {
        public string Id { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;

        private string _iconUrl = null!;
        public string IconUrl
        {
            get => _iconUrl;
            set => SetProperty(ref _iconUrl, value);
        }

        private decimal _pricaInUsd;
        public decimal PriceInUSD
        {
            get => _pricaInUsd;
            set => SetProperty(ref _pricaInUsd, value);
        }

        private decimal _totalVolume;
        public decimal TotalVolume
        {
            get => _totalVolume;
            set => SetProperty(ref _totalVolume, value);
        }

        private DateTime _lastTimeUpdate;
        public DateTime LastUpdatedTime
        {
            get => _lastTimeUpdate;
            set => SetProperty(ref _lastTimeUpdate, value);
        }
    }
}
