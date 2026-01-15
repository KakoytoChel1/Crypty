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
            YAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => $"${value.ToString("0.####")}",
                    Name = "Price",
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

        private CoinDetails? _selectedCoinDetails;
        public CoinDetails? SelectedCoinDetails
        {
            get => _selectedCoinDetails;
            set => SetProperty(ref _selectedCoinDetails, value);
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

        private void UpdateXChartModeTo24h()
        {
            if (SelectedCoinDetails == null)
                return;

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => new DateTime((long)value).ToString("HH:mm"),
                    Name = "Time"
                }
            };

            if (YAxes != null && YAxes.Length > 0)
            {
                YAxes[0].MinLimit = null;
                YAxes[0].MaxLimit = null;
            }

            if (HistoryPointSeries != null)
            {
                HistoryPointSeries.Clear();

                HistoryPointSeries.Add(
                new LineSeries<HistoryPoint>
                {
                    Values = SelectedCoinDetails.CoinHistoryPer24h,
                    Mapping = (historyPoint, chartPoint) => new(historyPoint.Time.Ticks, (double)historyPoint.Price),
                    GeometrySize = 0,
                    Fill = new SolidColorPaint(new SKColor(ApplicationState.RgbCode[0], ApplicationState.RgbCode[1], ApplicationState.RgbCode[2], 50)),
                    Stroke = new SolidColorPaint(new SKColor(ApplicationState.RgbCode[0], ApplicationState.RgbCode[1], ApplicationState.RgbCode[2]), 2)
                });
            }
        }

        private void UpdateXChartModeTo7d()
        {
            if (SelectedCoinDetails == null)
                return;

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labeler = value => new DateTime((long)value).ToString("dd.MM HH:mm"),
                    Name = "Time"
                }
            };

            if (YAxes != null && YAxes.Length > 0)
            {
                YAxes[0].MinLimit = null;
                YAxes[0].MaxLimit = null;
            }

            if (HistoryPointSeries != null)
            {
                HistoryPointSeries.Clear();

                HistoryPointSeries.Add(
                new LineSeries<HistoryPoint>
                {
                    Values = SelectedCoinDetails.CoinHistoryPer7d,
                    Mapping = (historyPoint, chartPoint) => new(historyPoint.Time.Ticks, (double)historyPoint.Price),
                    GeometrySize = 0,
                    Fill = new SolidColorPaint(new SKColor(ApplicationState.RgbCode[0], ApplicationState.RgbCode[1], ApplicationState.RgbCode[2], 50)),
                    Stroke = new SolidColorPaint(new SKColor(ApplicationState.RgbCode[0], ApplicationState.RgbCode[1], ApplicationState.RgbCode[2]), 2)
                });
            }
        }
        #endregion
    }
}
