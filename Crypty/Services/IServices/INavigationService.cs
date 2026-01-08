using System.Windows.Controls;

namespace Crypty.Services.IServices
{
    public interface INavigationService
    {
        // Interactions with root frame
        void InitializeRootFrame(Frame frame);
        void ChangePage<TPage>() where TPage : Page;
        void GoBack();

        // Interactions with specified frame
        void ChangePage<TPage>(Frame frame) where TPage : Page;

        void GoBack(Frame frame);
    }
}
