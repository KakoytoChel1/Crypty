using Crypty.ViewModels;
using System.Windows.Controls;

namespace Crypty.Views.Pages
{
    public partial class MainPage : Page
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            DataContext = mainPageViewModel;
        }
    }
}
