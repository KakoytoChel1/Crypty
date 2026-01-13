using Crypty.Models.DataModels;
using Crypty.ViewModels.Tools;

namespace Crypty.ViewModels
{
    public class AppState : ObservableObject
    {
        #region Properties

        private CoinDetails? _selectedCoin;
        public CoinDetails? SelectedCoin
        {
            get => _selectedCoin;
            set => SetProperty(ref _selectedCoin, value);
        }

        private string? _selectedCoinId;
        public string? SelectedCoinId
        {
            get => _selectedCoinId;
            set => SetProperty(ref _selectedCoinId, value);
        }

        #endregion
    }
}
