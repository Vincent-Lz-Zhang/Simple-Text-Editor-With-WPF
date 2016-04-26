using System.Windows;
using System.ComponentModel;

namespace TotaraEditor
{
    /// <summary>
    /// Interaction logic for FontSettingWindow.xaml
    /// </summary>
    public partial class FontSettingWindow : Window
    {
        public FontSettingWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void ResetBtn_Clicked(object sender, RoutedEventArgs e)
        {
            FontSetting fontsetting = this.DataContext as FontSetting;
            if (null != fontsetting)
            {
                fontsetting.SetToDefault();
            }
        }

        private void OKBtn_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
