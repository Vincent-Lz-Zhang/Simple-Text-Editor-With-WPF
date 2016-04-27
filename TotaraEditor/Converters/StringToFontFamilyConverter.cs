using System;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;

namespace TotaraEditor
{
    class StringToFontFamilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FontFamily fontfamily = new FontFamily(FontSetting.DEFAULT_FONT_FAMILY);
            if (value != null)
            {
                fontfamily = new FontFamily(value.ToString());
            }
            return fontfamily;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                FontFamily fontfamily = value as FontFamily;
                if (fontfamily != null)
                {
                    return fontfamily.Source;
                }
            }
            return null;
        }
    }
}
