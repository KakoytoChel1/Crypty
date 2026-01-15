using Crypty.Models.DataModels;
using Crypty.Services.IServices;
using Crypty.ViewModels.Tools;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Crypty.ViewModels
{
    public class CoinDetailsPageViewModel : ViewModelBase
    {
        public CoinDetailsPageViewModel(IConfigurationService configurationService, ICoinDataProviderService currencyDataproviderService, INavigationService navigationService, AppState appState) : base(configurationService, currencyDataproviderService, navigationService, appState)
        {
            // Reinitialize YAxes to set the currency format and position
            YAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => $"${value.ToString("0.####")}",
                    Position = AxisPosition.End
                }
            };

            HistoryPointSeries = new ObservableCollection<ISeries>();
        }

        #region Properties

        private ObservableCollection<ISeries>? _historyPointSeries;
        public ObservableCollection<ISeries>? HistoryPointSeries
        {
            get => _historyPointSeries;
            set => SetProperty(ref _historyPointSeries, value);
        }

        private Axis[]? _xAxes;
        public Axis[]? XAxes
        {
            get => _xAxes;
            set => SetProperty(ref _xAxes, value);
        }

        private Axis[]? _yAxes;
        public Axis[]? YAxes
        {
            get => _yAxes;
            set => SetProperty(ref _yAxes, value);
        }

        // Represent selected coin with all details (what is displayed on another page)
        private CoinDetails? _selectedCoinDetails;
        public CoinDetails? SelectedCoinDetails
        {
            get => _selectedCoinDetails;
            set => SetProperty(ref _selectedCoinDetails, value);
        }

        // Chart mode (24h or 7d)
        private string? _selectedChartMode;
        public string? SelectedChartMode
        {
            get => _selectedChartMode;
            set => SetProperty(ref _selectedChartMode, value);
        }
        #endregion


        #region Commands

        private ICommand? _goBackCommand;
        public ICommand GoBackCommand
        {
            get
            {
                return _goBackCommand ??= new RelayCommand(obj =>
                {
                    NavigationService.GoBack();

                    SelectedCoinDetails = null;
                });
            }
        }

        // Command for specified button to refresh data
        private ICommand? _refreshDataCommand;
        public ICommand RefreshDataCommmand
        {
            get
            {
                return _refreshDataCommand ??= new RelayCommand(async obj =>
                {
                    if (!string.IsNullOrWhiteSpace(ApplicationState.SelectedCoinId))
                    {
                        await RequestAndLoadData(ApplicationState.SelectedCoinId);
                    }
                });
            }
        }

        // Command for selecting 24h chart mode
        private ICommand? _select24hChartModeCommmand;
        public ICommand Select24hChartModeCommmand
        {
            get
            {
                return _select24hChartModeCommmand ??= new RelayCommand(async obj =>
                {
                    if (!string.IsNullOrWhiteSpace(ApplicationState.SelectedCoinId))
                    {
                        UpdateXChartModeTo24h();
                    }
                });
            }
        }

        // Command for selecting 7d chart mode
        private ICommand? _select7dChartModeCommmand;
        public ICommand Select7dChartModeCommmand
        {
            get
            {
                return _select7dChartModeCommmand ??= new RelayCommand(obj =>
                {
                    UpdateXChartModeTo7d();
                });
            }
        }

        // Command to follow ticker link (to market)
        private ICommand? _followTickerLinkCommand;
        public ICommand FollowTickerLinkCommand
        {
            get
            {
                return _followTickerLinkCommand ??= new RelayCommand(obj =>
                {
                    if(obj is Ticker ticker && !string.IsNullOrWhiteSpace(ticker.TradePlatformUrl))
                    {
                        var processStartInfo = new ProcessStartInfo(ticker.TradePlatformUrl)
                        {
                            UseShellExecute = true
                        };

                        Process.Start(processStartInfo);
                    }
                });
            }
        }

        // Command to request and load data for selected coin
        private ICommand? _requestAndLoadDataCommand;
        public ICommand RequestAndLoadDataCommand
        {
            get
            {
                return _requestAndLoadDataCommand ??= new RelayCommand(async obj =>
                {
                    if (!string.IsNullOrWhiteSpace(ApplicationState.SelectedCoinId))
                    {
                        await RequestAndLoadData(ApplicationState.SelectedCoinId);
                    }
                });
            }
        }
        #endregion


        #region Methods

        /// <summary>
        /// Asynchronously retrieves the latest details and historical data for the specified coin and updates the
        /// selected coin information.
        /// </summary>
        private async Task RequestAndLoadData(string coinId)
        {
            var updatedDetails = await CoinDataProviderService.GetCoinDataByIdAsync(coinId);
            if (updatedDetails != null)
            {
                SelectedCoinDetails = updatedDetails;

                var historyPointsPer24h = await CoinDataProviderService.GetCoinHistory(coinId, 1);
                var historyPointsPer7d = await CoinDataProviderService.GetCoinHistory(coinId, 7);

                SelectedCoinDetails.CoinHistoryPer24h = new ObservableCollection<HistoryPoint>(historyPointsPer24h ?? new List<HistoryPoint>());
                SelectedCoinDetails.CoinHistoryPer7d = new ObservableCollection<HistoryPoint>(historyPointsPer7d ?? new List<HistoryPoint>());

                UpdateXChartModeTo24h();
            }
        }

        /// <summary>
        /// Configures the chart to display data in 24-hour mode, updating the X-axis, Y-axis limits, and series to
        /// reflect the selected coin's 24-hour history.
        /// </summary>
        private void UpdateXChartModeTo24h()
        {
            if (SelectedCoinDetails == null)
                return;

            SelectedChartMode = "24h";

            // Reinitialize XAxes for 24h mode
            XAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => new DateTime((long)value).ToString("HH:mm"),
                }
            };

            // Reset YAxes limits (to reset "camera's" position)
            if (YAxes != null && YAxes.Length > 0)
            {
                YAxes[0].MinLimit = null;
                YAxes[0].MaxLimit = null;
            }

            // Update series for 24h data
            if (HistoryPointSeries != null)
            {
                HistoryPointSeries.Clear();

                HistoryPointSeries.Add(
                new LineSeries<HistoryPoint>
                {
                    Values = SelectedCoinDetails.CoinHistoryPer24h,
                    Mapping = (historyPoint, chartPoint) => new(historyPoint.Time.Ticks, (double)historyPoint.Price), // (x, y)
                    GeometrySize = 0, // Size of point on chart
                    Fill = new SolidColorPaint(new SKColor(ApplicationState.RgbCode[0], ApplicationState.RgbCode[1], ApplicationState.RgbCode[2], 50)), // Fill color with transparency (50%)
                    Stroke = new SolidColorPaint(new SKColor(ApplicationState.RgbCode[0], ApplicationState.RgbCode[1], ApplicationState.RgbCode[2]), 2) // Stroke color and its width
                });
            }
        }

        /// <summary>
        /// Configures the chart to display data for the last 7 days on the X-axis and updates related chart settings
        /// and series accordingly.
        /// </summary>
        private void UpdateXChartModeTo7d()
        {
            if (SelectedCoinDetails == null)
                return;

            SelectedChartMode = "7d";

            // Reinitialize XAxes for 7d mode
            XAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => new DateTime((long)value).ToString("dd.MM HH:mm"),
                }
            };

            // Reset YAxes limits (to reset "camera's" position)
            if (YAxes != null && YAxes.Length > 0)
            {
                YAxes[0].MinLimit = null;
                YAxes[0].MaxLimit = null;
            }

            // Update series for 7d data
            if (HistoryPointSeries != null)
            {
                HistoryPointSeries.Clear();

                HistoryPointSeries.Add(
                new LineSeries<HistoryPoint>
                {
                    Values = SelectedCoinDetails.CoinHistoryPer7d,
                    Mapping = (historyPoint, chartPoint) => new(historyPoint.Time.Ticks, (double)historyPoint.Price), // (x, y)
                    GeometrySize = 0, // Size of point on chart
                    Fill = new SolidColorPaint(new SKColor(ApplicationState.RgbCode[0], ApplicationState.RgbCode[1], ApplicationState.RgbCode[2], 50)), // Fill color with transparency (50%)
                    Stroke = new SolidColorPaint(new SKColor(ApplicationState.RgbCode[0], ApplicationState.RgbCode[1], ApplicationState.RgbCode[2]), 2) // Stroke color and its width
                });
            }
        }
        #endregion
    }
}
