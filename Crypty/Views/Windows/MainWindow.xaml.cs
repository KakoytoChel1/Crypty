using Crypty.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;

namespace Crypty
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var coinProvider = App.ServiceProvider.GetService<ICoinDataProviderService>();

            if (coinProvider == null)
                return;

            var topCoins = await coinProvider.GetTopPopularCoinPreviewsAsync(10);

            StringBuilder sb = new StringBuilder();

            foreach (var coin in topCoins!)
            { 
            
                sb.AppendLine($"{coin.Name} ({coin.Symbol}): {coin.Price} USD");
            }

            MessageBox.Show(sb.ToString());
        }

        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            var coinProvider = App.ServiceProvider.GetService<ICoinDataProviderService>();

            if (coinProvider == null)
                return;

            var coinDetails = await coinProvider.GetCoinDataById("bitcoin");

            MessageBox.Show($"{coinDetails!.Name} - {coinDetails.MarketData.CurrentPriceData["usd"]} USD");
        }
    }
}