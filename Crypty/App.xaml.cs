using System.Windows;

namespace Crypty
{
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        override protected void OnStartup(StartupEventArgs e)
        {
            // Service initialization
            IntializeServices();

            // Main window initialization
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            base.OnStartup(e);
        }

        private void IntializeServices()
        {

        }
    }

}
