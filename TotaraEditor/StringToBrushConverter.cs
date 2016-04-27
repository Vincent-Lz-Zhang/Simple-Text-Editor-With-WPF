using System.Windows.Media;
using System.Windows.Data;

namespace TotaraEditor
{
    class StringToBrushConverter : IValueConverter
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
            /*
            object obj = ((System.Reflection.PropertyInfo)value).GetValue(this, null);
            return (SolidColorBrush)new BrushConverter().ConvertFromString(obj.ToString());
            */
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
