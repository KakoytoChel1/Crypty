using Crypty.Services.IServices;

namespace Crypty.ViewModels
{
    public class CoinDetailsPageViewModel : ViewModelBase
    {
        public CoinDetailsPageViewModel(IConfigurationService configurationService, ICoinDataProviderService currencyDataproviderService, INavigationService navigationService, AppState appState) : base(configurationService, currencyDataproviderService, navigationService, appState)
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
