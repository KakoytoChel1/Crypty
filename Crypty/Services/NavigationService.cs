using Crypty.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Crypty.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        private Frame? _frame;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #region Root Frame Interactions

        public void InitializeRootFrame(Frame frame)
        {
            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
        }

        public void ChangePage<TPage>() where TPage : Page
        {
            if (_frame == null)
            {
                throw new InvalidOperationException("Root frame is not initialized. Call InitializeFrame first.");
            }

            var pageInstance = _serviceProvider.GetRequiredService<TPage>();

            pageInstance.KeepAlive = false;

            _frame.Navigate(pageInstance);
        }

        public void GoBack()
        {
            if (_frame != null && _frame.CanGoBack)
            {
                _frame.GoBack();
            }
        }
        #endregion


        #region Specified Frame Interactions
        public void ChangePage<TPage>(Frame frame) where TPage : Page
        {
            if (frame == null)
            {
                throw new ArgumentNullException(nameof(frame));
            }

            var pageInstance = _serviceProvider.GetRequiredService<TPage>();

            pageInstance.KeepAlive = false;

            frame.Navigate(pageInstance);
        }

        public void GoBack(Frame? frame)
        {
            if (frame != null && frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
        #endregion
    }
}
