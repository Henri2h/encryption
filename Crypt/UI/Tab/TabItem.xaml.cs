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
using MahApps.Metro.Controls;

namespace Code.UI.Tab
{
    /// <summary>
    /// Interaction logic for TabItem.xaml
    /// </summary>
    public partial class TabItem : MetroTabItem
    {
        public static int tabCount;
        public bool saved;
        public bool isXml;
        public bool isModified;
        public bool isCrypt;

        public Method.CryptMethod CryptMethod = Method.CryptMethod.clear;
        public Method.CryptFileMethod CryptFileMethod = Method.CryptFileMethod.content;

        public string fileName;
        public List<byte> augmentKey = new List<byte>();

        private string name;
        private const string defaultName = "new tab";

        public TabItem(string fileNameInput = defaultName)
        {

            InitializeComponent();
            if (fileNameInput != defaultName)
            {
                this.Header = System.IO.Path.GetFileName(fileNameInput);
            }
            else
            {
                this.Header = fileNameInput;
            }

            UITbText.SpellCheck.IsEnabled = true;
            FrameworkElement input = new FrameworkElement()
            {
                Language = System.Windows.Markup.XmlLanguage.GetLanguage("fr-FR")
            };
            UITbText.Language = input.Language;

            fileName = fileNameInput;
            isModified = true;
            isXml = false;
            isCrypt = false;
            augmentKey = null;
            CryptMethod = Method.CryptMethod.clear;
            tabCount++;
        }
        public bool Create()
        {
            return true;
        }

        public bool Close()
        {
            return true;
        }

        public bool Remove()
        {
            // tabControl.Items.Remove(selectedTabItem);
            return true;
        }

        public string NameFile
        {
            get { return name; }
            set { name = value; }
        }

        private void UITbText_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextEditor.inputText.define();
        }

        private void UITbText_TextChanged(object sender, TextChangedEventArgs e)
        {
            saved = false;
            isModified = true;
            TextEditor.inputText.define();
        }

        ~TabItem() { tabCount--; }
    }
}
