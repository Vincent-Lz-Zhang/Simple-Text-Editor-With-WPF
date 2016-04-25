using Xceed.Wpf.Toolkit;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace TotaraEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string appName = " - Totara Editor";
        private readonly string initFileName = "Untitled";
        private bool isContentUpdated = false;
        private string currentFilePath = "";

        /**************
         * Properties
         *************/

        private bool IsContentUpdated
        {
            set
            {
                if (this.isContentUpdated != value)
                {
                    this.isContentUpdated = value;
                    if (this.isContentUpdated)
                    {
                        this.status.Content = "The text has been modified...";
                    }
                    else
                    {
                        this.status.Content = "";
                    }
                }
            }
            get
            {
                return this.isContentUpdated;
            }
        }

        private string CurrentFilePath
        {
            set
            {
                if (this.currentFilePath != value)
                {
                    this.currentFilePath = value;
                    // update title of the main window
                    string name = this.currentFilePath.Substring(this.currentFilePath.LastIndexOf('\\') + 1);
                    this.Title = name + this.appName;
                }
            }
            get
            {
                return this.currentFilePath;
            }
        }


        /**************
         * Methods
         *************/
        public MainWindow()
        {
            InitializeComponent();
        }


        // event handlers of menu item
        private void Open_Executed(object sender, ExecutedRoutedEventArgs evt)//ExecutedRoutedEventArgs
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (true == result)
            {
                if (File.Exists(dlg.FileName))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(dlg.FileName))
                        {
                            String line = sr.ReadToEnd();
                            editor.Text = line;
                            // keep track of the opened file's path
                            this.CurrentFilePath = dlg.FileName;
                        }
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("The file could not be read:");
                        Console.WriteLine(exp.Message);
                    }
                }
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (this.IsContentUpdated)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.CurrentFilePath))
            {
                this.OverwriteFile(this.CurrentFilePath, this.editor.Text);
            }
            else
            {
                SaveAs_Executed(sender, e);
            }

        }

        private void SaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.CurrentFilePath) && string.IsNullOrWhiteSpace(this.editor.Text))
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.WriteTextToNewFile();
        }

        private void Format_MenuItem_Click(object sender, RoutedEventArgs e)
        {

            //editor.FontSize = 28;
            //editor.FontFamily = new FontFamily("Euphemia");
            //editor.Foreground = Brushes.GreenYellow;
            var res = Xceed.Wpf.Toolkit.MessageBox.Show(
"MsgConfirmDeleteSelectedRows",
"MsgTltConfirm",
MessageBoxButton.YesNoCancel,
MessageBoxImage.None, MessageBoxResult.No, null);
            Console.WriteLine("Res: " + res.ToString());
        }

        // other control event handlers
        private void editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("changed");
            this.IsContentUpdated = true;
        }



        // helpers

        private void WriteTextToNewFile()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = this.initFileName;
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (true == result)
            {
                this.OverwriteFile(dlg.FileName, this.editor.Text);
            }
        }

        private void OverwriteFile(string filePath, string text)
        {
            try
            {
                File.WriteAllText(filePath, text);  // overwrite if it exists, otherwise create it
                this.CurrentFilePath = filePath;
                this.IsContentUpdated = false;
            }
            catch (Exception exp)
            {
                Console.WriteLine("The file could not be written:");
                Console.WriteLine(exp.Message);
            }
        }


        private void ShowError(string errMsg)
        {

        }


        
    }
}
