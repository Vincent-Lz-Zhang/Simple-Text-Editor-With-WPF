using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Data;

namespace TotaraEditor
{
    class FontColorConversions : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BrushConverter conv = new BrushConverter();
            SolidColorBrush brush = conv.ConvertFromString("Lavender") as SolidColorBrush;
            if (null != value)
            {
                brush = conv.ConvertFromString(value.ToString()) as SolidColorBrush;
            }
            return brush;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
