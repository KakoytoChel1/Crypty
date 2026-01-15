using Crypty.Models.DataModels;
using Crypty.Services.IServices;
using Crypty.ViewModels.Tools;
using Crypty.Views.Pages;
using System.Collections.ObjectModel;
using System.Windows;
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
                    var coinPreviews = await CoinDataProviderService.GetTopPopularCoinPreviewsAsync(20);
                    if (coinPreviews != null)
                    {
                        CoinPreviews.Clear();
                        foreach (var coinPreview in coinPreviews)
                        {
                            CoinPreviews.Add(coinPreview);
                        }
                    }
                });
            }
        }

        private ICommand? _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ??= new RelayCommand(async(obj) =>
                {
                    if(!string.IsNullOrWhiteSpace(SearchRequestText) && CoinPreviews.Any())
                    {
                        var coinPrev = CoinPreviews.FirstOrDefault
                        (c => c.Name.ToLower() == SearchRequestText.ToLower() || c.Symbol.ToLower() == SearchRequestText.ToLower());

                        if (!await CoinDataProviderService.Ping())
                        {
                            MessageBox.Show($"Unable to load data, please check your internet connection.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        if (coinPrev != null)
                        {
                            var coinId = coinPrev.Id;
                            ApplicationState.SelectedCoinId = coinId;
                            NavigationService.ChangePage<CoinDetailsPage>();
                        }
                        else
                        {
                            MessageBox.Show("Coin not found. Please check the name or symbol and try again.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                });
            }
        }

        private ICommand? _selectionCoinCommand;
        public ICommand SelectionCoinCommand
        {
            get
            {
                return _selectionCoinCommand ??= new RelayCommand(async (obj) =>
                {
                    if(obj is CoinPreview selectedCoin)
                    {
                        var coinId = selectedCoin.Id;
                        ApplicationState.SelectedCoinId = coinId;

                        if (!await CoinDataProviderService.Ping())
                        {
                            MessageBox.Show($"Unable to load data, please check your internet connection.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        NavigationService.ChangePage<CoinDetailsPage>();
                    }
                });
            }
        }
        #endregion

        #region Methods
        #endregion
    }
}
