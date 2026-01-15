using Crypty.Services;
using Crypty.Services.IServices;
using Crypty.ViewModels;
using Crypty.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Crypty
{
    //1. Поиск по названию или символу монеты
    //2. Графическая визуализация выбранного режима (24ч, 7д)
    //3. Перевлючение между темной и светлой темой
    //4. Смена локализации приложения
    //5. Сделать иконку для приложения

    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show($"An unexpected error occurred:\n{e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        override async protected void OnStartup(StartupEventArgs e)
        {
            // Service initialization
            IntializeServices();

            IConfigurationService configurationService = ServiceProvider.GetRequiredService<IConfigurationService>();
            INavigationService navigationService = ServiceProvider.GetRequiredService<INavigationService>();

            // Main window initialization
            MainWindow mainWindow = new MainWindow();
            navigationService.InitializeRootFrame(mainWindow.rootFrame); // Setting up root frame
            mainWindow.Show();

            // Navigate to main page
            navigationService.ChangePage<MainPage>();

            base.OnStartup(e);
        }

        private void IntializeServices()
        {
            var serviceCollection = new ServiceCollection();

            #region ViewModels

            serviceCollection.AddSingleton<AppState>();
            serviceCollection.AddSingleton<MainPageViewModel>();
            serviceCollection.AddSingleton<CoinDetailsPageViewModel>();
            serviceCollection.AddSingleton<TickerDetailsPageViewModel>();
            #endregion

            #region Pages

            serviceCollection.AddTransient<MainPage>();
            serviceCollection.AddTransient<CoinDetailsPage>();
            serviceCollection.AddTransient<TickerDetailsPage>();
            #endregion

            #region Dialogs
            #endregion

            #region Services

            serviceCollection.AddSingleton<IConfigurationService, ConfigurationService>();
            serviceCollection.AddSingleton<ICoinDataProviderService, CoinDataProviderService>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            #endregion

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
    }

}
