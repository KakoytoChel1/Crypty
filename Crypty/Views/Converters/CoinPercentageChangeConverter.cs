using System.Globalization;
using System.Windows.Data;

namespace Crypty.Views.Converters
{
    /// <summary>
    /// Provides a value converter that determines whether a coin's percentage change is positive or negative
    /// </summary>
    public class CoinPercentageChangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double percent)
            {
                if (percent < 0)
                    return false;
                else
                    return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
