using System;
using System.Text;
using System.Windows;
namespace Code.UI
{
    /// <summary>
    /// Logique d'interaction pour property.xaml
    /// </summary>
    public partial class property : Window
    {

        public property(string question, string defaultAnswer = "")
        {
            InitializeComponent();
            lblQuestion.Content = question;
            txtAnswer.Password = defaultAnswer;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

        }
        public Byte[] Answer
        {

            get
            {
                try { return Encoding.UTF8.GetBytes(txtAnswer.Password); }
                catch { MessageBox.Show("password was invalid"); }
                return null;
            }
        }
        public Method.CryptMethod Methods
        {
            get
            {
                

                if (rbCustom.IsChecked == true)
                {
                    return Method.CryptMethod.custom;
                }
                else if (rbAES.IsChecked == true)
                {
                    return Method.CryptMethod.AES;
                }
                else { return Method.CryptMethod.custom; }
            }
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        private void rbCustom_Checked(object sender, RoutedEventArgs e)
        {
            rbAES.IsChecked = false;
        }
        private void rbAES_Checked(object sender, RoutedEventArgs e)
        {
            rbCustom.IsChecked = false;
        }
    }
}
