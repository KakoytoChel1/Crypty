using Crypty.ViewModels;
using System.Windows.Controls;

namespace Crypty.Views.Pages
{
    public partial class CoinDetailsPage : Page
    {
        public CoinDetailsPage(CoinDetailsPageViewModel coinDetailsPageViewModel)
        {
            InitializeComponent();
            DataContext = coinDetailsPageViewModel;
        }
    }
}
