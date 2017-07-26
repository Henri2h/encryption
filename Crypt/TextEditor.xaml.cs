using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using MahApps.Metro.Controls;
using Code.UI;
using Code.Core;

namespace Code
{
    /// <summary>
    /// Logique d'interaction pour TextEditor.xaml
    /// </summary>
    ///
    public partial class TextEditor : MetroWindow
    {
        public static int selectedTabIndex = 0;
        //tab controll
        public static UI.Tab.TabItem current;
        public static TextEditor inputText;


        // file tab new routed command
        public static RoutedCommand File_new_command = new RoutedCommand();
        public static RoutedCommand File_save_command = new RoutedCommand();
        public static RoutedCommand File_close_command = new RoutedCommand();
        public static RoutedCommand File_open_command = new RoutedCommand();
        public static RoutedCommand File_reload_command = new RoutedCommand();

        // ======================= custom commands binding ================
        private void File_new_exe(object sender, ExecutedRoutedEventArgs e)
        {
            newTab(null);

        }
        private void File_save_exe(object sender, ExecutedRoutedEventArgs e)
        {
            FileEdit.Save();
        }
        private void File_close_exe(object sender, ExecutedRoutedEventArgs e)
        {
            FileEdit.Close();
        }
        private void File_open_exe(object sender, ExecutedRoutedEventArgs e)
        {
            FileEdit.Open();
        }
        private void File_reload_exe(object sender, ExecutedRoutedEventArgs e)
        {
            FileEdit.Load();
        }
        // ===================== input menu tab ===========================
        // ===== menu File =====
        private void File_save_Click(object sender, RoutedEventArgs e)
        {
            FileEdit.Save();
        }
        private void File_open_Click(object sender, RoutedEventArgs e)
        {
            FileEdit.Open();
        }
        private void File_new_Click(object sender, RoutedEventArgs e)
        {
            newTab(null);
        }
        private void File_close_Click(object sender, RoutedEventArgs e)
        {
            FileEdit.Close();
        }
        private void File_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(current.UITbText, "print content of the textbox");
            }
        }
        // ===== menu Crypt =====
        //      Crypt_crypt
        private void setKey_Click(object sender, RoutedEventArgs e)
        {
            property inputDialog = new property("Please enter augment :", "");
            if (inputDialog.ShowDialog() == true)
            {
                current.isXml = true;
                current.isCrypt = true;
                current.augmentKey = new List<byte>(inputDialog.Answer);
                current.CryptMethod = inputDialog.Methods;
                current.CryptFileMethod = Method.CryptFileMethod.content;
                define();
                FileEdit.Load();
            }
        }

        /// <summary>
        /// crypt decode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterKey_Click(object sender, RoutedEventArgs e)
        {
            property inputDialog = new property("Please enter augment :", "");
            if (inputDialog.ShowDialog() == true)
            {
                current.augmentKey = new List<byte>(inputDialog.Answer);
                current.isXml = true;
                current.isCrypt = true;
                current.CryptMethod = inputDialog.Methods;

                current.CryptFileMethod = Method.CryptFileMethod.content;

                define();
                FileEdit.Save();
            }
        }

        /// <summary>
        /// set the is crypted property to false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void noCode_Click(object sender, RoutedEventArgs e)
        {

            //toCrypt = false;
            current.isCrypt = false;
            FileEdit.Load();
            define();
        }

        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="FileName_in"></param>
        public TextEditor(string FileName_in)
        {
            // this is the main class and it is very small
            InitializeComponent();
            newTab(FileName_in);
            inputText = this;
        }


        /// <summary>
        /// Load the file
        /// </summary>
        public void newTab(string filename)
        {

            var newTabCustom = new UI.Tab.AnaliticsView();

       //     UI.Tab.TabItem newTabCustom = new UI.Tab.TabItem(filename);
         //   current = newTabCustom;
            tabControl.Items.Add(newTabCustom);
            tabControl.SelectedItem = newTabCustom;

            selectedTabIndex = tabControl.Items.IndexOf(current);

          //  define();

        }

        /// <summary>
        /// change the UI
        /// </summary>
        public void define()
        {
            if (current != null)
            {
                // ================================ buton save ========================================
                // enable the save button if isModified == true or disable it if isModified == false

                if (current.isModified == true)
                {
                    File_save.IsEnabled = true;
                }
                else
                {
                    File_save.IsEnabled = false;
                }

                // ================================ if crypt ==========================================
                if (current.isCrypt)
                {
                    tbIsCrypt.Text = "This text is crypt with key = *****";
                }
                else
                {
                    tbIsCrypt.Text = "This text isn't crypted";
                }
                // ==================================== header ========================================
                if (current.isModified)
                {

                    File_menu.Header = "_File*";
                    if (current.fileName != null && current.fileName != "")
                    {
                        current.Header = current.fileName + "* ";
                        Files_1.Header = current.fileName + "* ";

                    }
                    else
                    {
                        current.Header = "New File 1*";
                        Files_1.Header = "New File 1*";
                    }
                    Files_1.Background = Brushes.Red;
                }
                else
                {

                    current.Header = current.fileName;
                    File_menu.Header = "_File";
                    Files_1.Header = current.fileName;
                    Files_1.Background = Brushes.Green;
                }

                // ================================ define number of lines ============================

                tbLenght.Text = "Line : " + current.UITbText.LineCount.ToString();
                // ================================ cursor position ===================================

                /*int line = 0;
                int column = 0;
                int caret = textBox.CaretIndex;
                int iLine = textBox.GetLineIndexFromCharacterIndex(caret);
                if (iLine < 0) iLine = 0;
                line = iLine;
                int firstChar = textBox.GetCharacterIndexFromLineIndex(iLine);
                if (firstChar < 0) firstChar = 0;
                column = caret - firstChar;*/
                if (current.UITbText.IsKeyboardFocused)
                {
                    int s = current.UITbText.SelectionStart;
                    int y = current.UITbText.GetLineIndexFromCharacterIndex(s);
                    int x = s - current.UITbText.GetCharacterIndexFromLineIndex(y);
                    x++;
                    y++;
                    tbPosition.Text = "Cursor position = " + x + " Line number = " + y;
                }


                // ================================ is Xml ============================================
                if (current.isXml)
                {
                    tbIsXml.Text = "This text is a xml file";
                }
                else
                {
                    tbIsXml.Text = "This text isn't a xml file";
                }
            }
        }

        /// <summary>
        /// charge les données lorsque la fenetre est chargée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            FileEdit.Load();
            define();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (UI.Tab.TabItem te in tabControl.Items)
            {
                current = te;
                define();

                // if save == false
                if (te.saved == false)
                {
                    MessageBoxResult output = System.Windows.MessageBox.Show("ok do you wan't to close without saving ? " + current.fileName, "delete file?", MessageBoxButton.YesNoCancel);
                    string output_string = output.ToString();
                    if (output_string == "Cancel")
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    if (output_string == "No")
                    {
                        FileEdit.Save();
                        define();
                    }
                }
            }
        }
        private void add_textbox_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("click ok");
        }
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // tabControl.Items[selectedTabIndex] = current;
            current = tabControl.SelectedItem as UI.Tab.TabItem;
            selectedTabIndex = tabControl.Items.IndexOf(current);
            if (current == null)
            {
                tabControl.Items.MoveCurrentToPrevious();
                if (tabControl.HasItems == false)
                {
                    newTab("New tab");
                }
            }
            define();
        }

        private void btLanguage_Click(object sender, RoutedEventArgs e)
        {
            Language inputDialog = new Language();
            if (inputDialog.ShowDialog() == true)
            {
                current.UITbText.Language = inputDialog.language;
                define();
            }
            else
            {
                MessageBox.Show("Please select a language " + Environment.NewLine + "curent : " + current.UITbText.Language.ToString());
            }
        }
    }
}
