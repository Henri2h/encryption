using Microsoft.HockeyApp;
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

namespace Code
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // this is the default file that the program is going to open when he is launch
        string FileName = @"";


        public MainWindow()
        {
            LaunchHockeyReportingAsync();
            InitializeComponent();
            loadDefault();
        }
        public async void LaunchHockeyReportingAsync()
        {
            HockeyClient.Current.Configure("00fcc608b8904e13a9c6ea4c4ebfa762");
            await HockeyClient.Current.SendCrashesAsync();
        }


        private void btText_Click(object sender, RoutedEventArgs e)
        {
            //enter your default path
            //when the program is launch, he try to open this file
            var textEditor = new TextEditor(FileName);
            textEditor.Show();
            this.Close();
        }
        private void loadDefault()
        {
            FileName = Properties.Settings.Default.Default_document;

            try {
                string[] activationData = Environment.GetCommandLineArgs();
                if (activationData != null)
                {
                    var textEditor = new TextEditor(activationData[1]);
                    textEditor.Show();
                    this.Close();
                }
            }
            catch
            {
                if (FileName != "") { tbInfo.Text = "Default file : " + FileName; }
                else { tbInfo.Text = "Default file isn't defined"; }

            }
        }
        private void btDefaultPath_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".tdx";
            dlg.Filter = "text dialog xample (*.tdx)|*.tdx|txt (*.txt)|*.txt|others file|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                Properties.Settings.Default.Default_document = dlg.FileName;
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Please select a file!");
            }
            loadDefault();
        }
    }
}
