using Crypty.Services.IServices;
using Crypty.ViewModels.Tools;

namespace Crypty.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public INavigationService NavigationService { get; }
        public IConfigurationService ConfigurationService { get; }
        public ICoinDataProviderService CurrencyDataproviderService { get; }

        public ViewModelBase(IConfigurationService configurationService, ICoinDataProviderService currencyDataproviderService, INavigationService navigationService)
        {
            ConfigurationService = configurationService;
            CurrencyDataproviderService = currencyDataproviderService;
            NavigationService = navigationService;
        }
    }
}
