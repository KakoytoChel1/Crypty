using Crypty.Services.IServices;
using Crypty.Views.Pages;
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

            maximizeBtn.Visibility = Visibility.Visible;
            restoreBtn.Visibility = Visibility.Collapsed;

            rootFrame.Navigate(App.ServiceProvider.GetRequiredService<MainPage>());
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void restoreBtn_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        private void maximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void mainWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                maximizeBtn.Visibility = Visibility.Collapsed;
                restoreBtn.Visibility = Visibility.Visible;
                this.BorderThickness = new Thickness(7);
            }
            else
            {
                maximizeBtn.Visibility = Visibility.Visible;
                restoreBtn.Visibility = Visibility.Collapsed;
                this.BorderThickness = new Thickness(0);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}