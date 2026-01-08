using Crypty.ViewModels.Tools;
using System.Collections.ObjectModel;

namespace Crypty.Models.DataModels
{
    public class CoinDetails : ObservableObject
    {
        public string Id { get; set; } = null!;
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        private decimal _priceInUsd;
        public decimal PriceInUsd
        {
            get => _priceInUsd;
            set => SetProperty(ref _priceInUsd, value);
        }

        private ObservableCollection<Ticker> _tickers = null!;
        public ObservableCollection<Ticker> Tickers
        {
            get => _tickers;
            set => SetProperty(ref _tickers, value);
        }
        
        private List<List<double>> _coinHistory = null!;
        public List<List<double>> CoinHistory
        {
            get => _coinHistory;
            set => SetProperty(ref _coinHistory, value);
        }
    }
}
