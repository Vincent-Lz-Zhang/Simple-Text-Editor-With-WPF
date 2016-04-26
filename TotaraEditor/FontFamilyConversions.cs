using System.Windows.Media;
using System.Windows.Data;

namespace TotaraEditor
{
    class FontFamilyConversions : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FontFamily fontfamily = new FontFamily("Arial");
            if (value != null)
            {
                fontfamily = new FontFamily(value.ToString());
            }
            return fontfamily;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
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
