using Crypty.Services.IServices;

namespace Crypty.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(IConfigurationService configurationService, ICoinDataProviderService currencyDataproviderService, INavigationService navigationService) : base(configurationService, currencyDataproviderService, navigationService)
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
