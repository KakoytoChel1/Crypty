using Crypty.Models.DataModels;
using Crypty.ViewModels.Tools;
using System.Windows.Media;

namespace Crypty.ViewModels
{
    /// <summary>
    /// Represents the way to store application-wide state information
    /// </summary>
    public class AppState : ObservableObject
    {
        public AppState()
        {
            AccentColorBrush = new SolidColorBrush(Color.FromRgb(RgbCode[0], RgbCode[1], RgbCode[2]));
        }

        #region Properties

        public byte[] RgbCode { get; } = { 64, 93, 230 };
        public SolidColorBrush? AccentColorBrush { get; private set; }

        private string? _selectedCoinId;
        public string? SelectedCoinId
        {
            get => _selectedCoinId;
            set => SetProperty(ref _selectedCoinId, value);
        }
        #endregion
    }
}
