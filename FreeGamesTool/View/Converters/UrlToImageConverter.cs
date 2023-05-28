using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace FreeGamesTool.View.Converters
{
    public class UrlToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Check if the value parameter is a valid URL string
            if (value is string url && Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                // Create the BitmapImage from the URL
                var bitmap = new BitmapImage(new Uri(url));

                // Return the BitmapImage
                return bitmap;
            }

            // If the value is not a valid URL string, return null
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
