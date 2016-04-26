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
                    if (string.IsNullOrEmpty(this.currentFilePath))
                    {
                        this.Title = this.initFileName + this.appName;
                    }
                    else
                    {
                        this.Title = this.ExtractFileName(this.currentFilePath) + this.appName;
                    }
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

        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs evt)
        {
            if (string.IsNullOrEmpty(this.CurrentFilePath) && !this.IsContentUpdated)
            {
                evt.CanExecute = false;
            }
            else
            {
                evt.CanExecute = true;
            }
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs evt)
        {
            if (this.isContentUpdated)
            {
                var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                            "You have a document unsaved, would you like to save it before you go somewhere else?",
                            "Confirm dialog",
                            MessageBoxButton.YesNoCancel,
                            MessageBoxImage.None,
                            MessageBoxResult.Cancel,
                            null
                        );
                if ("Cancel" == res.ToString())
                {
                    // nothing
                }
                else if ("No" == res.ToString())
                {
                    this.GoBackToInitialState();
                }
                else if ("Yes" == res.ToString())
                {
                    this.Save_Executed(sender, evt);
                }
                else
                {
                    ShowError("I believe something goes wrong. You may need to restart Totara Editor.");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.CurrentFilePath))
                {
                    // nothing, should not reach here
                }
                else
                {
                    this.GoBackToInitialState();
                }
            }
        }

        // event handlers of menu item
        private void Open_Executed(object sender, ExecutedRoutedEventArgs evt)//ExecutedRoutedEventArgs
        {
            if (this.isContentUpdated)
            {
                var res = Xceed.Wpf.Toolkit.MessageBox.Show(    
                            "You have a document unsaved, would you like to save it before you go somewhere else?", 
                            "Confirm dialog",
                            MessageBoxButton.YesNoCancel,
                            MessageBoxImage.None, 
                            MessageBoxResult.Cancel, 
                            null
                        );
                if ("Cancel" == res.ToString())
                {
                    // nothing
                }
                else if ("No" == res.ToString())
                {
                    this.OpenFileWithBrowser();
                }
                else if ("Yes" == res.ToString())
                {
                    this.Save_Executed(sender, evt);
                }
                else
                {
                    ShowError("I believe something goes wrong. You may need to restart Totara Editor.");
                }
            }
            else
            {
                this.OpenFileWithBrowser();
            }
            
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs evt)
        {
            if (this.IsContentUpdated)
            {
                evt.CanExecute = true;
            }
            else
            {
                evt.CanExecute = false;
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs evt)
        {
            if (this.IsContentUpdated)
            {
                if (!string.IsNullOrEmpty(this.CurrentFilePath))
                {
                    this.OverwriteFile(this.CurrentFilePath, this.editor.Text);
                }
                else
                {
                    this.SaveAs_Executed(sender, evt);
                }
            }
        }

        private void SaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs evt)
        {
            //if (string.IsNullOrEmpty(this.CurrentFilePath) && string.IsNullOrWhiteSpace(this.editor.Text))
            if (string.IsNullOrEmpty(this.CurrentFilePath) && !this.IsContentUpdated)
            {
                evt.CanExecute = false;
            }
            else
            {
                evt.CanExecute = true;
            }
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs evt)
        {
            if (!string.IsNullOrEmpty(this.CurrentFilePath) || this.IsContentUpdated)
            {
                this.WriteTextToNewFile();
            }
        }

        private void Format_MenuItem_Click(object sender, RoutedEventArgs evt)
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
        private void editor_TextChanged(object sender, TextChangedEventArgs evt)
        {
            //Console.WriteLine("changed");
            this.IsContentUpdated = true;
            //ShowError("This is a mock error message.");
        }



        // helpers

        /// <summary>
        /// take the program back to its initial state, in which a untitled new document is opened
        /// </summary>
        private void GoBackToInitialState()
        {
            this.CurrentFilePath = "";
            this.editor.Text = "";
            this.IsContentUpdated = false;
            
        }

        private void OpenFileWithBrowser()
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
                            //
                            this.IsContentUpdated = false;
                        }
                    }
                    catch (Exception exp)
                    {
                        ShowError("The file could not be read: " + exp.Message);
                    }
                }
            }
        }

        private void WriteTextToNewFile()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if (string.IsNullOrEmpty(this.CurrentFilePath))
            {
                dlg.FileName = this.initFileName;
            }
            else
            {
                dlg.FileName = this.StripExtension(this.ExtractFileName(this.currentFilePath));
            }
            
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
                this.status.Content = "The file has been saved successfully.";  // TODO: move it somewhere else
            }
            catch (Exception exp)
            {
                ShowError("The file could not be written: " + exp.Message);
            }
        }


        private string ExtractFileName(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return "";
            }
            else
            {
                if (path.LastIndexOf('\\')>=0)
                {
                    return path.Substring(path.LastIndexOf('\\') + 1);
                }
                else
                {
                    return path;
                }
            }
        }

        private string StripExtension(string fullName)
        {
            if (!string.IsNullOrEmpty(fullName))
            {
                if (fullName.LastIndexOf('.') > 0)
                {
                    return fullName.Substring(0, fullName.LastIndexOf('.'));
                }
                else
                {
                    return fullName;
                }
            }
            return this.initFileName;
        }

        private void ShowError(string errMsg)
        {
                var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                            errMsg,
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error, 
                            MessageBoxResult.No, 
                            null
                        );
        }


        
    }
}
