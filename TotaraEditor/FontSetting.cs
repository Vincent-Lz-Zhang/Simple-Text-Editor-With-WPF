using System.ComponentModel;

namespace TotaraEditor
{
    class FontSetting : INotifyPropertyChanged
    {
        private string _fontFamily = "Arial";   // initial font
        private string _fontColor = "Lavender";   // initial color
        private int _fontSize = 12;   // initial font size
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
