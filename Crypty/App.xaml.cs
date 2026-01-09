using Crypty.Services;
using Crypty.Services.IServices;
using Crypty.ViewModels;
using Crypty.Views.Pages;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Crypty
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        override async protected void OnStartup(StartupEventArgs e)
        {
            // Service initialization
            IntializeServices();

            IConfigurationService configurationService = ServiceProvider.GetRequiredService<IConfigurationService>();

            // Main window initialization
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

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
