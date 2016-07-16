using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace Code
{
    public class customTabItem : TabItem
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

        public TextBox textbox;

        private string name;

        // Constructor
        public customTabItem(string fileNameInput = "new tab")
        {
            this.Header = fileNameInput;
            
            textbox = new TextBox();
            textbox.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            textbox.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

            textbox.AcceptsReturn = true;
            textbox.AcceptsTab = true;
            textbox.TextWrapping = TextWrapping.Wrap;

            textbox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textbox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

            textbox.SelectionChanged += textBox_SelectionChanged;
            textbox.TextChanged += textBox_TextChanged;

            textbox.SpellCheck.IsEnabled = true;
            FrameworkElement input = new FrameworkElement();
            input.Language = System.Windows.Markup.XmlLanguage.GetLanguage("fr-FR");
            textbox.Language = input.Language;

            fileName = fileNameInput;
            isModified = true;
            isXml = false;
            isCrypt = false;
            augmentKey = null;
            CryptMethod = Method.CryptMethod.clear;

            // Creating the Grid (create Canvas or StackPanel or other panel here)
            Grid grid = new Grid();
            grid.Children.Add(textbox);     // Add more controls
            this.Content = grid;
            tabCount++;
        }

        ~customTabItem()
        {
            tabCount--;
        }


        public bool create()
        {

            return true;
        }

        public bool close()
        {
            return true;
        }
        public bool remove()
        {
            // tabControl.Items.Remove(selectedTabItem);
            return true;
        }
        public string NameFile
        {
            get { return name; }
            set { name = value; }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            saved = false;
            isModified = true;
            TextEditor.inputText.define();
        }

        private void textBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            TextEditor.inputText.define();
        }

    }
}
