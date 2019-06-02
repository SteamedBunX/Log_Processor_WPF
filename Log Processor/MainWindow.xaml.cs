
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Log_Processing_Component;
using System.Diagnostics;

namespace Log_Processor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Processor p;
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Button_LoadFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                p = new Processor(dlg.FileName);
                p.Process();
                TextBox_PostProcessLog.Text = p.GetFullResultString();
            }
        }

        private void Button_Print_NoLog_Click(object sender, RoutedEventArgs e)
        {
            string currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = currentDirectory + @"/ResultLog.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                File.WriteAllText(path, p.GetTimeResultString().Replace("\n", Environment.NewLine));
            }
            else
            {
                File.WriteAllText(path, p.GetTimeResultString().Replace("\n", Environment.NewLine));
            }
            Process.Start(path);
        }

        private void Button_Print_WithLog_Click(object sender, RoutedEventArgs e)
        {
            string currentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            string path = currentDirectory + @"/ResultLog.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                File.WriteAllText(path, p.GetFullResultString().Replace("\n", Environment.NewLine));
            }
            else
            {
                File.WriteAllText(path, p.GetFullResultString().Replace("\n", Environment.NewLine));
            }
            Process.Start(path);
        }
    }
}
