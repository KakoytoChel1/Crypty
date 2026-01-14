using System.Globalization;
using System.Windows.Data;

namespace Crypty.Views.Converters
{
    public class CoinDescriptionLanguageSelectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Dictionary<string, string> data)
            {
                if(data.TryGetValue("en", out string? result))
                {
                    return result;
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
