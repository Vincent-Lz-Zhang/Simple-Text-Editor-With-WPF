using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.ComponentModel;
using System.Security;

namespace TotaraEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string appName = " - Totara Notepad";
        private readonly string initFileName = "Untitled";
        private bool isContentUpdated = false;
        private string currentFilePath = "";
        private FontSetting viewModelFontSetting = new FontSetting();
        private FontSettingWindow settingsWindow = null;

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

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.ConfirmQuit(null, null);
        }

        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs evt)
        {
            if (string.IsNullOrEmpty(this.CurrentFilePath))
            {
                if (this.IsContentUpdated)
                {
                    if (string.IsNullOrEmpty(this.editor.Text))
                    {
                        evt.CanExecute = false;
                    }
                    else
                    {
                        evt.CanExecute = true;
                    }
                }
                else
                {
                    evt.CanExecute = false;
                }
                
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
                if (string.IsNullOrEmpty(this.CurrentFilePath) && string.IsNullOrEmpty(this.editor.Text))
                {
                    //
                }
                else
                {
                    var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                           "You have a document unsaved, would you like to save it before you go somewhere else?",
                           "Confirm dialog",
                           MessageBoxButton.YesNoCancel,
                           MessageBoxImage.None,
                           MessageBoxResult.Cancel,
                           (Style)App.Current.Resources["TotaraMessageBoxStyle"]
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
                        this.ShowError("I believe something goes wrong. You may need to restart Totara Editor.");
                    }
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
                if (string.IsNullOrEmpty(this.CurrentFilePath) && string.IsNullOrEmpty(this.editor.Text))
                {
                    this.OpenFileWithBrowser();
                }
                else
                {
                    var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                            "You have a document unsaved, would you like to save it before you go somewhere else?",
                            "Confirm dialog",
                            MessageBoxButton.YesNoCancel,
                            MessageBoxImage.None,
                            MessageBoxResult.Cancel,
                            (Style)App.Current.Resources["TotaraMessageBoxStyle"]
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
                        this.ShowError("I believe something goes wrong. You may need to restart Totara Editor.");
                    }
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
                if (string.IsNullOrEmpty(this.CurrentFilePath) && string.IsNullOrEmpty(this.editor.Text))
                {
                    evt.CanExecute = false;
                }
                else
                {
                    evt.CanExecute = true;
                }
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
                if (string.IsNullOrEmpty(this.CurrentFilePath))
                {
                    if (string.IsNullOrEmpty(this.editor.Text))
                    {
                        //
                    }
                    else
                    {
                        this.SaveAs_Executed(sender, evt);
                    }
                }
                else
                {
                    this.OverwriteFile(this.CurrentFilePath, this.editor.Text);
                }
            }
        }

        private void SaveAs_CanExecute(object sender, CanExecuteRoutedEventArgs evt)
        {
            if (!string.IsNullOrEmpty(this.CurrentFilePath) )
            {
                evt.CanExecute = true;
            }
            else
            {
                if (this.IsContentUpdated)
                {
                    if (string.IsNullOrEmpty(this.editor.Text))
                    {
                        evt.CanExecute = false;
                    }
                    else
                    {
                        evt.CanExecute = true;
                    }
                }
                else
                {
                    evt.CanExecute = false;
                }
            }
        }
        
        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs evt)
        {
            if (!string.IsNullOrEmpty(this.CurrentFilePath))
            {
                this.WriteTextToNewFile();
            }
            else
            {
                if (this.IsContentUpdated)
                {
                    if (string.IsNullOrEmpty(this.editor.Text))
                    {
                        //
                    }
                    else
                    {
                        this.WriteTextToNewFile();
                    }
                }
                else
                {
                    //
                }
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
                            (Style)App.Current.Resources["TotaraMessageBoxStyle"]
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
                            this.ShowError("The file could not be deleted because the path is too long.");
                        }
                        catch (UnauthorizedAccessException ex)
                        {
                            this.ShowError("The file could not be deleted because your account doesn't have the permission.");
                        }
                        catch (IOException ex)
                        {
                            this.ShowError("The file could not be deleted because it is held by another process.");
                        }
                        catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is DirectoryNotFoundException || ex is NotSupportedException)
                        {
                            this.ShowError("The file could not be deleted because the path is invalid.");
                        }
                        catch (Exception ex)
                        {
                            this.ShowError("The file could not be deleted. " + ex.Message);
                        }
                    }
                }
                else
                {
                    this.ShowError("I believe something goes wrong. You may need to restart Totara Editor.");
                }
            }
        }

        private void Quit_Executed(object sender, ExecutedRoutedEventArgs evt)
        {
            this.ConfirmQuit(sender, evt);
        }

        private void Format_MenuItem_Click(object sender, RoutedEventArgs evt)
        {
            if (null == this.settingsWindow)
            {
                this.settingsWindow = new FontSettingWindow();
                this.settingsWindow.DataContext = this.viewModelFontSetting;
            }
            this.settingsWindow.Show();
        }

        private void About_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWin = new AboutWindow();
            aboutWin.ShowDialog();
        }

        // other control event handlers
        private void editor_TextChanged(object sender, TextChangedEventArgs evt)
        {
            this.IsContentUpdated = true;
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
                    catch (OutOfMemoryException exp)
                    {
                        this.ShowError("The file could not be read, because available memory is insufficient.");
                    }
                    catch (IOException exp)
                    {
                        this.ShowError("The file could not be read: " + exp.Message);
                    }
                    catch (Exception exp)
                    {
                        this.ShowError("The file could not be read: " + exp.Message);
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
            catch (PathTooLongException ex)
            {
                this.ShowError("The file could not be written because the path is too long.");
            }
            catch (IOException ex)
            {
                this.ShowError("The file could not be written because it is held by another process.");
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is SecurityException)
            {
                this.ShowError("The file could not be written because your account doesn't have the permission.");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is DirectoryNotFoundException || ex is NotSupportedException)
            {
                this.ShowError("The file could not be written because the path is invalid.");
            }
            catch (Exception exp)
            {
                this.ShowError("The file could not be written: " + exp.Message);
            }
        }

        private void ConfirmQuit(object sender, ExecutedRoutedEventArgs evt)
        {
            if (!this.IsContentUpdated || (string.IsNullOrEmpty(this.CurrentFilePath) && string.IsNullOrEmpty(this.editor.Text)))
            {
                this.Quit();
            }
            else
            {
                var res = Xceed.Wpf.Toolkit.MessageBox.Show(
                            "You have a document unsaved, would you like to save it before you quit?",
                            "Confirm dialog",
                            MessageBoxButton.YesNoCancel,
                            MessageBoxImage.None,
                            MessageBoxResult.Cancel,
                            (Style)App.Current.Resources["TotaraMessageBoxStyle"]
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
                    this.Save_Executed(sender, evt);
                }
                else
                {
                    this.ShowError("I believe something goes wrong. You may need to restart Totara Editor.");
                }
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
                if (path.LastIndexOf('\\') >= 0)
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
                        (Style)App.Current.Resources["TotaraMessageBoxStyle"]
                    );
        }

    }
}
