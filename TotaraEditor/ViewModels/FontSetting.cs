using System.ComponentModel;

namespace TotaraEditor
{
    class FontSetting : INotifyPropertyChanged
    {
        public const string DEFAULT_FONT_FAMILY = "Arial";      // initial font
        public const string DEFAULT_FONT_COLOR = "Lavender";    // initial color
        public const int DEFAULT_FONT_SIZE = 12;                // initial font size

        private string _fontFamily = DEFAULT_FONT_FAMILY;
        private string _fontColor = DEFAULT_FONT_COLOR;
        private int _fontSize = DEFAULT_FONT_SIZE;

        public event PropertyChangedEventHandler PropertyChanged;

        public string FontFamily
        {
            get
            {
                return _fontFamily;
            }
            set
            {
                _fontFamily = value;
                OnPropertyChanged("FontFamily");
            }
        }

        public string FontColor
        {
            get
            {
                return _fontColor;
            }
            set
            {
                _fontColor = value;
                OnPropertyChanged("FontColor");
            }
        }

        public int FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                OnPropertyChanged("FontSize");
            }
        }

        public void SetToDefault()
        {
            this.FontColor = DEFAULT_FONT_COLOR;
            this.FontFamily = DEFAULT_FONT_FAMILY;
            this.FontSize = DEFAULT_FONT_SIZE;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
