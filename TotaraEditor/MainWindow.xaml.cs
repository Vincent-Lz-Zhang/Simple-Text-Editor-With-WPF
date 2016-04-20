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
        private string currentFilePath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Format_MenuItem_Click(object sender, RoutedEventArgs e)
        {

            //editor.FontSize = 28;
            editor.FontFamily = new FontFamily("Euphemia");
            //editor.Foreground = Brushes.GreenYellow;
        }

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
                            this.currentFilePath = dlg.FileName;

                            string name = this.currentFilePath.Substring(this.currentFilePath.LastIndexOf('\\') + 1);

                            this.Title = name + this.appName;

                            editor.Text = line;
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


        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.currentFilePath))
            {
                try
                {
                    File.WriteAllText(this.currentFilePath, this.editor.Text);
                }
                catch (Exception exp)
                {
                    Console.WriteLine("The file could not be written:");
                    Console.WriteLine(exp.Message);
                }
            }
            else
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = "";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Text documents (.txt)|*.txt";

                Nullable<bool> result = dlg.ShowDialog();

                if (true == result)
                {
                    try
                    {
                        //FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                        File.WriteAllText(dlg.FileName, this.editor.Text);
                        this.currentFilePath = dlg.FileName;
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine("The file could not be written:");
                        Console.WriteLine(exp.Message);
                    }
                }

            }

        }




        }
    }
