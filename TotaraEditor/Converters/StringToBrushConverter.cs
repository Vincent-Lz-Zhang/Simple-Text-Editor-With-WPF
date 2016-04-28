using System.Windows.Media;
using System.Windows.Data;

namespace TotaraEditor
{
    class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BrushConverter conv = new BrushConverter();
            SolidColorBrush brush = (SolidColorBrush)conv.ConvertFromString(FontSetting.DEFAULT_FONT_COLOR);
            if (null != value)
            {
                SolidColorBrush temp = conv.ConvertFromString(value.ToString()) as SolidColorBrush;
                if (null != temp)
                {
                    brush = temp;
                }
            }
            return brush;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
