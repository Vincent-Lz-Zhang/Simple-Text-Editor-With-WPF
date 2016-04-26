using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            this.Closing += FontSettingWindow_Closing;
        }

        private void FontSettingWindow_Closing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("Sasha");
        }
    }
}
