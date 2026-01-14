using Crypty.Services.IServices;
using Crypty.ViewModels.Tools;

namespace Crypty.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public INavigationService NavigationService { get; }
        public IConfigurationService ConfigurationService { get; }
        public ICoinDataProviderService CoinDataProviderService { get; }
        public AppState ApplicationState { get; }

        public ViewModelBase(IConfigurationService configurationService, ICoinDataProviderService coinDataproviderService, INavigationService navigationService, AppState appState)
        {
            ConfigurationService = configurationService;
            CoinDataProviderService = coinDataproviderService;
            NavigationService = navigationService;
            ApplicationState = appState;
        }
    }
}
