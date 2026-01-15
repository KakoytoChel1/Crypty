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

        // Collection of coin previews to be displayed on the main page
        public ObservableCollection<CoinPreview> CoinPreviews { get; private set; }

        // Search request text entered by the user
        private string _searchRequestText = string.Empty;
        public string SearchRequestText
        {
            get => _searchRequestText;
            set => SetProperty(ref _searchRequestText, value);
        }
        #endregion

        #region Commands

        // Command to request and load coins list
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

        // Command to search for a coin based on user input
        private ICommand? _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ??= new RelayCommand(async(obj) =>
                {
                    if(!string.IsNullOrWhiteSpace(SearchRequestText) && CoinPreviews.Any())
                    {
                        // Find the coin preview that matches name (Bitcoin) or symbol (BTC)
                        var coinPreview = CoinPreviews.FirstOrDefault
                        (c => c.Name.ToLower() == SearchRequestText.ToLower() || c.Symbol.ToLower() == SearchRequestText.ToLower());

                        // Check ability to connect to the data provider
                        if (!await CoinDataProviderService.Ping())
                        {
                            MessageBox.Show($"Unable to load data, please check your internet connection.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        // If found, navigate to the coin details page
                        if (coinPreview != null)
                        {
                            var coinId = coinPreview.Id;
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

        // Command to handle selection (click) of a coin from the list
        private ICommand? _selectionCoinCommand;
        public ICommand SelectionCoinCommand
        {
            get
            {
                return _selectionCoinCommand ??= new RelayCommand(async (obj) =>
                {
                    if(obj is CoinPreview selectedCoin)
                    {
                        // Store id of the selected coin in application state
                        var coinId = selectedCoin.Id;
                        ApplicationState.SelectedCoinId = coinId;

                        // Check ability to connect to the data provider
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
    }
}
