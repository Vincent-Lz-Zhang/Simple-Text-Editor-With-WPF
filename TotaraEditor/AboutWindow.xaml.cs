using System.Windows;
using System.Windows.Navigation;

namespace TotaraEditor
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Console.WriteLine(e.Uri.ToString());
            System.Diagnostics.Process.Start(e.Uri.ToString());
            
        }
    }
}
