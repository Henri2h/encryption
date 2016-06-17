using System.Windows;
using System.Windows.Markup;

namespace Code
{
    /// <summary>
    /// Interaction logic for Language.xaml
    /// </summary>
    public partial class Language : Window
    {
        public Language()
        {
            InitializeComponent();

            languageBox.SelectedIndex = languageBox.Items.Add("English");
            languageBox.Items.Add("Français");
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        /// <summary>
        /// return the selected language
        /// </summary>
        public XmlLanguage language
        {
            get
            {
                string selectItem = languageBox.SelectedItem.ToString();
                XmlLanguage output = XmlLanguage.GetLanguage("en-US");
                if (selectItem == "English")
                {
                    output = XmlLanguage.GetLanguage("en-US");
                }
                else if (selectItem == "Français")
                {
                    output = XmlLanguage.GetLanguage("fr-FR");
                }
                else
                {
                    output = XmlLanguage.GetLanguage("en-US");
                }
                return output;
            }
        }
    }
}
