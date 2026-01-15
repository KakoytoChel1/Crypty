using System.Windows.Controls;

namespace Crypty.Services.IServices
{
    public interface INavigationService
    {
        #region Interactions with root frame

        /// <summary>
        /// Initializes the application's root frame with the specified frame instance
        /// </summary>
        /// <remarks>This method should be called during application startup to set up navigation and
        /// visual structure. The provided frame will be used for navigation throughout the application's
        /// lifetime.</remarks>
        /// <param name="frame">The frame to use as the application's root frame. Cannot be null.</param>
        void InitializeRootFrame(Frame frame);

        /// <summary>
        /// Navigates to a new page of the specified type within the root frame
        /// </summary>
        /// <typeparam name="TPage">The type of page to navigate to. Must inherit from <see cref="Page"/>.</typeparam>
        void ChangePage<TPage>() where TPage : Page;

        /// <summary>
        /// Navigates to the previous page in the navigation history, if available
        /// </summary>
        void GoBack();
        #endregion

        #region Interactions with specified frame
        /// <summary>
        /// Navigates the specified frame to a new page of type <typeparamref name="TPage"/>
        /// </summary>
        /// <typeparam name="TPage">The type of the page to navigate to. Must derive from <see cref="Page"/>.</typeparam>
        /// <param name="frame">The frame in which to display the new page. Cannot be null.</param>
        void ChangePage<TPage>(Frame frame) where TPage : Page;

        /// <summary>
        /// Navigates the specified frame to the previous page in its navigation history, if possible
        /// </summary>
        /// <param name="frame">The frame instance whose navigation history will be traversed backward. Cannot be null.</param>
        void GoBack(Frame frame);
        #endregion
    }
}
