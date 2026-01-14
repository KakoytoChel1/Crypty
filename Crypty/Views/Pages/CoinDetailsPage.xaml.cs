using Crypty.ViewModels;
using System.Windows.Controls;

namespace Crypty.Views.Pages
{
    public partial class CoinDetailsPage : Page
    {
        private readonly CoinDetailsPageViewModel _viewModel;

        public CoinDetailsPage(CoinDetailsPageViewModel coinDetailsPageViewModel)
        {
            InitializeComponent();
            _viewModel = coinDetailsPageViewModel;
            DataContext = _viewModel;

            this.Loaded += CoinDetailsPage_Loaded;
        }

        private void CoinDetailsPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.RequestAndLoadDataCommand.Execute(null);
        }
    }
}
