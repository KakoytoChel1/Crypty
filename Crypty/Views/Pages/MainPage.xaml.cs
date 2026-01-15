using Crypty.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Crypty.Views.Pages
{
    public partial class MainPage : Page
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            _viewModel = mainPageViewModel;
            DataContext = _viewModel;

            this.Loaded += MainPage_Loaded;
            coinListBox.SelectionChanged += CoinListBox_SelectionChanged;
        }

        private void CoinListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectionCoinCommand.Execute(coinListBox.SelectedItem);

            coinListBox.SelectionChanged -= CoinListBox_SelectionChanged;
            coinListBox.SelectedItem = null;
            coinListBox.SelectionChanged += CoinListBox_SelectionChanged;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.RequestAndLoadDataCommand.Execute(null);
        }

        private void searchBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                _viewModel.SearchCommand.Execute(null);
            }
        }
    }
}
