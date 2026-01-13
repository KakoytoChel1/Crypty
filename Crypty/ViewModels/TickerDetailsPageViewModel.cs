using Crypty.Services.IServices;

namespace Crypty.ViewModels
{
    public class TickerDetailsPageViewModel : ViewModelBase
    {
        public TickerDetailsPageViewModel(IConfigurationService configurationService, ICoinDataProviderService currencyDataproviderService, INavigationService navigationService, AppState appState) : base(configurationService, currencyDataproviderService, navigationService, appState)
        {
        }

        #region Properties
        #endregion

        #region Commands
        #endregion

        #region Methods
        #endregion
    }
}
