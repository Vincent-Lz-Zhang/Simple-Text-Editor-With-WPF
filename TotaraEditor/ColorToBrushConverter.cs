using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media;
using System.Reflection;

namespace TotaraEditor
{
    class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object obj = ((PropertyInfo)value).GetValue(this, null);
            return (SolidColorBrush)new BrushConverter().ConvertFromString(obj.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
