using Crypty.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;

namespace Crypty
{
    public partial class MainWindow : Window
    {
        private App _currentApplication;
        private Collection<ResourceDictionary> _mergedResources;
        private readonly int _currentThemeIndex = 0;
        private IConfigurationService _configurationService;

        public MainWindow()
        {
            InitializeComponent();

            maximizeBtn.Visibility = Visibility.Visible;
            restoreBtn.Visibility = Visibility.Collapsed;

            _configurationService = App.ServiceProvider.GetRequiredService<IConfigurationService>();

            _currentApplication = (App)Application.Current;

            _mergedResources = _currentApplication.Resources.MergedDictionaries;

            
            var currentTheme = _configurationService.Get<string>("theme");

            if(currentTheme == "dark")
                selectDark();
            else
                selectLight();
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

        private void lightThemeSwitchBtn_Click(object sender, RoutedEventArgs e)
        {
            selectLight();
            _configurationService.Set("theme", "light");
        }

        private void darkThemeSwitchBtn_Click(object sender, RoutedEventArgs e)
        {
            selectDark();
            _configurationService.Set("theme", "dark");
        }

        private void selectLight()
        {
            if (_mergedResources.Count > 0)
            {
                _mergedResources.RemoveAt(_currentThemeIndex);
                var newDictionary = new ResourceDictionary();
                newDictionary.Source = new Uri("Views/Resources/Themes/LightTheme.xaml", UriKind.Relative);
                _mergedResources.Insert(_currentThemeIndex, newDictionary);

                lightThemeSwitchBtn.BorderThickness = new Thickness(1);
                darkThemeSwitchBtn.BorderThickness = new Thickness(0);
            }
        }

        private void selectDark()
        {
            if (_mergedResources.Count > 0)
            {
                _mergedResources.RemoveAt(_currentThemeIndex);
                var newDictionary = new ResourceDictionary();
                newDictionary.Source = new Uri("Views/Resources/Themes/DarkTheme.xaml", UriKind.Relative);
                _mergedResources.Insert(_currentThemeIndex, newDictionary);

                lightThemeSwitchBtn.BorderThickness = new Thickness(0);
                darkThemeSwitchBtn.BorderThickness = new Thickness(1);
            }
        }

        private void engLanguageSwitchBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ukrLanguageSwitchBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rusLanguageSwitchBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}