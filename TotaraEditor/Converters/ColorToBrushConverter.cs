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
            BrushConverter conv = new BrushConverter();
            SolidColorBrush brush = (SolidColorBrush)conv.ConvertFromString(FontSetting.DEFAULT_FONT_COLOR);
            if (null != value)
            {
                PropertyInfo pi = value as PropertyInfo;
                if (null != pi)
                {
                    SolidColorBrush temp = conv.ConvertFromString(pi.Name) as SolidColorBrush;
                    if (null != temp)
                    {
                        brush = temp;
                    }
                }
            }
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
