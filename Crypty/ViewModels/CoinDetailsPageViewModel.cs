using Crypty.Services.IServices;

namespace Crypty.ViewModels
{
    internal class CoinDetailsPageViewModel : ViewModelBase
    {
        public CoinDetailsPageViewModel(IConfigurationService configurationService, ICoinDataProviderService currencyDataproviderService, INavigationService navigationService) : base(configurationService, currencyDataproviderService, navigationService)
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
