using Crypty.Services.IServices;
using Crypty.ViewModels.Tools;

namespace Crypty.ViewModels
{
    /// <summary>
    /// Provides a base class for view models that supplies access to navigation, configuration, coin data provider
    /// services, and application state.
    /// </summary>
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
