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
        private FontSetting viewModelFontSetting = new FontSetting();

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
            this.DataContext = this.viewModelFontSetting;
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

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs evt)
        {
            if (string.IsNullOrEmpty(this.CurrentFilePath))
            {
                evt.CanExecute = false;
            }
            else
            {
                evt.CanExecute = true;
            }
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs evt)
        {
            if (!string.IsNullOrEmpty(this.CurrentFilePath))
            {
                var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                            "Are you sure you want to delete " + this.ExtractFileName(this.CurrentFilePath) + "?",
                            "Confirm dialog",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.None,
                            MessageBoxResult.Cancel,
                            null
                        );
                if ("Cancel" == res.ToString())
                {
                    // nothing
                }
                else if ("OK" == res.ToString())
                {
                    if (File.Exists(this.CurrentFilePath))
                    {
                        try
                        {
                            File.Delete(this.CurrentFilePath);
                            this.GoBackToInitialState();
                            this.status.Content = "The text has been deleted.";
                        }
                        catch (PathTooLongException ex)
                        {
                            ShowError("The file could not be deleted because the path is too long.");
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            ShowError("The file could not be deleted because your account doesn't have the permission.");
                        }
                        catch (IOException ex)
                        {
                            ShowError("The file could not be deleted because it is held by another process.");
                        }
                        catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is DirectoryNotFoundException || ex is NotSupportedException)
                        {
                            ShowError("The file could not be deleted because the path is invalid.");
                        }
                        catch (Exception ex)
                        {
                            ShowError("The file could not be deleted. " + ex.Message);
                        }
                    }
                }
                else
                {
                    ShowError("I believe something goes wrong. You may need to restart Totara Editor.");
                }
            }
        }

        private void Quit_MenuItem_Click(object sender, RoutedEventArgs evt)
        {
            if (this.IsContentUpdated)
            {
                var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                            "You have a document unsaved, would you like to save it before you quit?",
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
                    this.Quit();
                }
                else if ("Yes" == res.ToString())
                {
                    this.Save_Executed(sender, null);
                }
                else
                {
                    ShowError("I believe something goes wrong. You may need to restart Totara Editor.");
                }
            }
            else
            {
                this.Quit();
            }
        }

        private void Format_MenuItem_Click(object sender, RoutedEventArgs evt)
        {

            //editor.FontSize = 28;
            //editor.FontFamily = new FontFamily("Euphemia");
            //editor.Foreground = Brushes.GreenYellow;
            /*
            var res = Xceed.Wpf.Toolkit.MessageBox.Show(
"MsgConfirmDeleteSelectedRows",
"MsgTltConfirm",
MessageBoxButton.YesNoCancel,
MessageBoxImage.None, MessageBoxResult.No, null);
            Console.WriteLine("Res: " + res.ToString());
            */

            var settingsWindow = new FontSettingWindow();
            settingsWindow.DataContext = this.viewModelFontSetting;
            settingsWindow.Show();

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

        /// <summary>
        /// let user pick up a file to open through file browser 
        /// </summary>
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

        /// <summary>
        /// save the text of editor to either to an opened file or a new file
        /// </summary>
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

        /// <summary>
        /// if the file passed exists, then overwrite the text to it, otherwise create it, and write the text
        /// </summary>
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

        /// <summary>
        /// quit the WPF application
        /// </summary>
        private void Quit()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// extract the files name from its full path
        /// </summary>
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

        /// <summary>
        /// strip off the extension name from a file name
        /// </summary>
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

        /// <summary>
        /// pop up a error dialog, displaying the passed message
        /// </summary>
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
