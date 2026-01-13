using Crypty.Models.DataModels;
using Crypty.Services.IServices;
using Crypty.ViewModels.Tools;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Crypty.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(IConfigurationService configurationService, ICoinDataProviderService currencyDataproviderService, INavigationService navigationService, AppState appState) : base(configurationService, currencyDataproviderService, navigationService, appState)
        {
            CoinPreviews = new ObservableCollection<CoinPreview>();
        }

        #region Properties

        public ObservableCollection<CoinPreview> CoinPreviews { get; private set; }

        private string _searchRequestText = string.Empty;
        public string SearchRequestText
        {
            get => _searchRequestText;
            set => SetProperty(ref _searchRequestText, value);
        }
        #endregion

        #region Commands

        private ICommand? _requestAndLoadDataCommand;
        public ICommand RequestAndLoadDataCommand
        {
            get
            {
                return _requestAndLoadDataCommand ??= new RelayCommand(async (obj) =>
                {
                    //var coinPreviews = await CurrencyDataproviderService.GetTopPopularCoinPreviewsAsync(10);
                    //if (coinPreviews != null)
                    //{
                    //    CoinPreviews.Clear();
                    //    foreach (var coinPreview in coinPreviews)
                    //    {
                    //        CoinPreviews.Add(coinPreview);
                    //    }
                    //}
                });
            }
        }

        private ICommand? _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ??= new RelayCommand((obj) =>
                {
                    
                });
            }
        }

        private ICommand? _selectionCoinCommand;
        public ICommand SelectionCoinCommand
        {
            get
            {
                return _selectionCoinCommand ??= new RelayCommand((obj) =>
                {
                    if(obj is CoinPreview selectedCoin)
                    {
                        var coinId = selectedCoin.Id;
                        ApplicationState.SelectedCoinId = coinId;
                    }
                });
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
