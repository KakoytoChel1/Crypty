using System.Globalization;
using System.Windows.Data;

namespace Crypty.Views.Converters
{
    public class TickerMarketDataConverter : IValueConverter
    {
        /// <summary>
        /// Provides a value converter that extracts the USD price from a dictionary of coin market data for use
        /// </summary>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Dictionary<string, decimal> data)
            {
                if (data.TryGetValue("usd", out decimal result))
                {
                    return result;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
