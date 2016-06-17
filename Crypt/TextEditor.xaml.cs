using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Xml;

namespace Code
{
    /// <summary>
    /// Logique d'interaction pour TextEditor.xaml
    /// </summary>
    ///

    public partial class TextEditor : Window
    {

        int tabNumber = 0;
        private List<TabItem> _tabItems;
        TabItem selectedTabItem;
        TextBox textBox;
        bool isXml = false;
        bool isModified = false;
        string FileName = @"C:\";
        bool save = false;
        bool toCrypt = false;
        Byte[] augmentKey;
        string CryptMethod = "custom";


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
            FileSave();
        }
        private void File_close_exe(object sender, ExecutedRoutedEventArgs e)
        {
            FileClose();
        }
        private void File_open_exe(object sender, ExecutedRoutedEventArgs e)
        {
            FileOpen();
        }
        private void File_reload_exe(object sender, ExecutedRoutedEventArgs e)
        {
            FileLoad();
        }
        // ===================== input menu tab ===========================
        // ===== menu File =====
        //      File_save
        private void File_save_Click(object sender, RoutedEventArgs e)
        {
            FileSave();
        }
        //      File_open
        private void File_open_Click(object sender, RoutedEventArgs e)
        {
            FileOpen();
        }
        //      File_new
        private void File_new_Click(object sender, RoutedEventArgs e)
        {
            newTab(null);
        }
        //      File_close
        private void File_close_Click(object sender, RoutedEventArgs e)
        {
            FileClose();
        }

        private void File_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(textBox, "print content of the textbox");
            }
        }
        // ===== menu Crypt =====
        //      Crypt_crypt
        private void setKey_Click(object sender, RoutedEventArgs e)
        {
            property inputDialog = new property("Please enter augment :", "");
            if (inputDialog.ShowDialog() == true)
            {
                augmentKey = inputDialog.Answer;
                textBox.Resources[5] = augmentKey;
                textBox.Resources[4] = true;
                // Crypt Method
                textBox.Resources[6] = inputDialog.Method;
                define();
                FileLoad();
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
                augmentKey = inputDialog.Answer;
                // is XML is true
                textBox.Resources[3] = true;
                // toCrypt = true;
                textBox.Resources[4] = true;
                textBox.Resources[5] = augmentKey;
                // Crypt Method
                textBox.Resources[6] = inputDialog.Method;

                define();
                FileSave();
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
            textBox.Resources[3] = false;
            FileLoad();
            define();
        }

        /// <summary>
        /// Main function
        /// </summary>
        /// <param name="FileName_in"></param>
        public TextEditor(string FileName_in)
        {
            FileName = FileName_in;
            InitializeComponent();
            _tabItems = new List<TabItem>();
            newTab(FileName_in);
        }

        /// <summary>
        /// File open
        /// </summary>
        private void FileOpen()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".tdx";
            dlg.Filter = "tdx (*.tdx)|*.tdx|others file (*.*)|*.*";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document
                newTab(dlg.FileName);
                FileLoad();
            }
        }

        /// <summary>
        /// Load the file
        /// </summary>
        private void FileLoad()
        {
            if (File.Exists(FileName))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(FileName);
                    StringBuilder sb = new StringBuilder();
                    doc.Normalize();
                    XmlNodeList nodeList;
                    XmlNode root = doc.DocumentElement;
                    nodeList = root.SelectNodes("//Text");
                    foreach (XmlNode xmlNode in nodeList)
                    {
                        string text = xmlNode.InnerText;
                        // if coded
                        // if toCrypt == true
                        if (bool.Parse(textBox.Resources[4].ToString()) == true)
                        {
                            // =================================================
                            //                     De crypt
                            // =================================================
                            string[] stringBytes = text.Split(' ');
                            List<int> intBytes = new List<int>();

                            foreach (string stringByte in stringBytes)
                            {
                                if (stringByte != "")
                                {
                                    intBytes.Add(Convert.ToInt32(stringByte));
                                }
                            }
                            List<byte> textBytes = new List<byte>();
                            foreach (int intByte in intBytes)
                            {
                                textBytes.Add(Convert.ToByte(intByte));
                            }

                            code code1 = new code();
                            byte[] ByteInput = textBytes.ToArray();
                            byte[] ByteOutput = new byte[ByteInput.Length];
                            if (CryptMethod == "custom")
                            {
                                // charge the code class witch allow to crypt and decrypt the text
                                ByteOutput = code1.crypt_decode(ByteInput, augmentKey);
                            }
                            else
                            if (CryptMethod == "AES")
                            {
                                ByteOutput = code1.AESDecrypt(ByteInput, augmentKey);
                            }
                            text = Encoding.UTF8.GetString(ByteOutput);
                        }
                        sb.AppendLine(text);
                    }
                    textBox.Text = sb.ToString();
                    // isxml = true
                    textBox.Resources[3] = true;
                }
                catch
                {
                    textBox.Text = File.ReadAllText(FileName, Encoding.UTF8);
                    MessageBox.Show("This wasn' a xml file !");
                    isXml = false;
                }
                // save = true
                textBox.Resources[1] = true;
                //isModified = false;
                textBox.Resources[2] = false;
                define();
            }
            else
            {
                textBox.Text = "File doesn't exist";
                // save = false
                textBox.Resources[1] = false;
            }
        }

        /// <summary>
        /// close tab and the file
        /// </summary>
        private void FileClose()
        {
            // if save == false
            if (bool.Parse(textBox.Resources[1].ToString()) == false)
            {
                MessageBoxResult output = MessageBox.Show("ok do you wan't to close without saving ? " + textBox.Resources[0].ToString(), "delete file?", MessageBoxButton.YesNoCancel);
                string output_string = output.ToString();
                if (output_string == "Cancel") { }
                else
                if (output_string == "No")
                {
                    FileSave();
                    define();
                    tabControl.Items.Remove(selectedTabItem);
                }
                if (output_string == "Yes") { tabControl.Items.Remove(selectedTabItem); }
            }
            else
            {
                tabControl.Items.Remove(selectedTabItem);
            }
        }

        private void chooseFileName()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".tdx";
            dlg.Filter = "text dialog xample (*.tdx)|*.tdx|txt (*.txt)|*.txt|others file|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                // save the file name
                textBox.Resources[0] = dlg.FileName;
                define();
                FileSave();
            }
            else
            {
                MessageBox.Show("Please select a file!");
            }
        }

        private void FileSave()
        {

            if (FileName == "" || FileName == null || FileName == "New File")
            {
                chooseFileName();
            }
            else
            {
                //save the file
                string output = "This variable store the output text that is going to be write in the output file";
                if (isXml == true)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Clear();
                    sb.AppendLine("<?xml version= \"1.0\" encoding= \"UTF-8\"?>");
                    sb.AppendLine("<Documents>");
                    using (StringReader sr = new StringReader(textBox.Text))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            //please insert if the code has to be encrypted
                            if (toCrypt == true)
                            {
                                // =========================================
                                //            Crypt code
                                // =========================================

                                code code1 = new code();
                                byte[] ByteInput = Encoding.UTF8.GetBytes(line);
                                byte[] ByteOutput = new byte[ByteInput.Length];

                                if (CryptMethod == "custom")
                                {

                                    ByteOutput = code1.crypt_code(ByteInput, augmentKey);
                                }
                                if (CryptMethod == "AES")
                                {
                                    ByteOutput = code1.AESEncrypt(ByteInput, augmentKey);
                                }

                                StringBuilder sw = new StringBuilder();
                                foreach (byte we in ByteOutput)
                                {
                                    sw.Append(Convert.ToInt32(we));
                                    sw.Append(" ");

                                }
                                line = sw.ToString();
                            }
                            sb.AppendLine("\t" + "<Text>" + line + "</Text>");
                        }
                    }
                    sb.AppendLine("</Documents>");
                    output = sb.ToString();
                }
                else
                {
                    // if the file isn't xml, it is save in plain text
                    output = textBox.Text;
                }
                File.WriteAllText(FileName, output, Encoding.Default);
                //set the output :
                // save = true
                textBox.Resources[1] = true;
                // isModified = false
                textBox.Resources[2] = false;
                define();
            }
        }

        private void newTab(string inputFileName)
        {
            // TextBox x:Name="textBox" HorizontalAlignment="Stretch" AcceptsTab="True" 
            //SpellCheck.IsEnabled="True" Language="fr-FR" AcceptsReturn="True" 
            //TextChanged ="textBox_TextChanged" TextWrapping="WrapWithOverflow"
            //VerticalScrollBarVisibility ="Auto" SelectionChanged="textBox_SelectionChanged"
            //UseLayoutRounding="False" Grid.ColumnSpan="2"

            int count = _tabItems.Count;
            // ==================== finaly add the tab to the tab container =======================
            TabItem input = createTabItem(inputFileName);
            tabControl.Items.Add(input);

            selectedTabItem = input;
            textBox = (TextBox)selectedTabItem.Content;
            define();
            tabControl.SelectedItem = input;
        }

        private TabItem createTabItem(string inputFileName)
        {
            tabNumber++;
            TabItem addTab = new TabItem();
            addTab.Header = string.Format("This is a new tab {0}", tabNumber);
            addTab.Name = "tab" + tabNumber.ToString();


            // ============================ define the new textbox ================================
            TextBox tb = new TextBox();
            tb.Name = string.Format("tb{0}", tabNumber);
            tb.Text = "";

            tb.HorizontalAlignment = HorizontalAlignment.Stretch;
            tb.VerticalAlignment = VerticalAlignment.Stretch;

            tb.AcceptsReturn = true;
            tb.AcceptsTab = true;
            tb.TextWrapping = TextWrapping.Wrap;

            tb.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            tb.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

            tb.SelectionChanged += textBox_SelectionChanged;
            tb.TextChanged += textBox_TextChanged;

            tb.SpellCheck.IsEnabled = true;
            FrameworkElement input = new FrameworkElement();
            input.Language = System.Windows.Markup.XmlLanguage.GetLanguage("fr-FR");
            tb.Language = input.Language;
            //       tb.Language = XmlLanguage.GetLanguage("en-US");


            // add the new tab

            // file name
            tb.Resources.Add(0, "New File");
            // save
            tb.Resources.Add(1, false);
            // is modified
            tb.Resources.Add(2, true);
            // isxml
            tb.Resources.Add(3, false);
            // is crypt
            tb.Resources.Add(4, false);
            // decode value key
            tb.Resources.Add(5, null);
            // Crypt Method
            tb.Resources.Add(6, "custom");
            // tb.Resources.Add(0, "");

            if (inputFileName != null)
            {

                tb.Resources[0] = inputFileName;


                inputFileName = null;
            }
            addTab.Content = tb;
            return addTab;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // save = false;
            textBox.Resources[1] = false;
            // isModified = true;
            textBox.Resources[2] = true;

            define();


        }

        /// <summary>
        /// change the UI
        /// </summary>
        private void define()
        {
            // ismodifeid
            FileName = textBox.Resources[0].ToString();
            save = bool.Parse(textBox.Resources[1].ToString());
            isModified = bool.Parse(textBox.Resources[2].ToString());
            isXml = bool.Parse(textBox.Resources[3].ToString());
            toCrypt = bool.Parse(textBox.Resources[4].ToString());
            CryptMethod = textBox.Resources[6].ToString();
            if (toCrypt)
            {

                augmentKey = textBox.Resources[5] as Byte[];
            }
            // ================================ buton save ========================================
            // enable the save button if isModified == true or disable it if isModified == false

            if (isModified == true)
            {
                File_save.IsEnabled = true;
            }
            else
            {
                File_save.IsEnabled = false;
            }

            // ================================ if crypt ==========================================
            if (toCrypt)
            {
                tbIsCrypt.Text = "This text is crypt with key = *****";
            }
            else
            {
                tbIsCrypt.Text = "This text isn't crypted";
            }
            // ==================================== header ========================================
            if (isModified)
            {
                File_menu.Header = "_File*";
                if (FileName != null)
                {
                    selectedTabItem.Header = FileName + "* ";
                    Files_1.Header = FileName + "* ";
                }
                else
                {
                    selectedTabItem.Header = "New File 1*";
                    Files_1.Header = "New File 1*";
                }
                Files_1.Background = Brushes.Red;
            }
            else
            {
                selectedTabItem.Header = FileName;
                File_menu.Header = "_File";
                Files_1.Header = FileName;
                Files_1.Background = Brushes.Green;
            }

            // ================================ define number of lines ============================
            tbLenght.Text = "Line : " + textBox.LineCount.ToString();
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
            if (textBox.IsKeyboardFocused)
            {


                int s = textBox.SelectionStart;
                int y = textBox.GetLineIndexFromCharacterIndex(s);
                int x = s - textBox.GetCharacterIndexFromLineIndex(y);
                x++;
                y++;
                tbPosition.Text = "Cursor position = " + x + " Line number = " + y;
            }


            // ================================ is Xml ============================================
            if (isXml)
            {
                tbIsXml.Text = "This text is a xml file";
            }
            else
            {
                tbIsXml.Text = "This text isn't a xml file";
            }
        }
        
        /// <summary>
        /// charge les données lorsque la fenetre est chargée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            FileLoad();
            define();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (TabItem te in tabControl.Items)
            {
                // if save == false
                if (bool.Parse(textBox.Resources[1].ToString()) == false)
                {
                    MessageBoxResult output = System.Windows.MessageBox.Show("ok do you wan't to close without saving ? " + textBox.Resources[0].ToString(), "delete file?", MessageBoxButton.YesNoCancel);
                    string output_string = output.ToString();
                    if (output_string == "Cancel")
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    if (output_string == "No")
                    {
                        FileSave();
                        define();
                    }
                }
            }
        }

        private void textBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            define();
        }

        private void add_textbox_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("click ok");
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTabItem = tabControl.SelectedItem as TabItem;
            if (selectedTabItem != null)
            {
                textBox = (TextBox)selectedTabItem.Content;
            }
            else
            {
                tabControl.Items.MoveCurrentToPrevious();
                if (tabControl.HasItems == false)
                {
                    newTab(null);
                }
                tabControl.SelectedItem = tabControl.Items.GetItemAt(0);
                selectedTabItem = tabControl.SelectedItem as TabItem;
            }
            define();
        }

        private void btLanguage_Click(object sender, RoutedEventArgs e)
        {
            Language inputDialog = new Language();
            if (inputDialog.ShowDialog() == true)
            {
                textBox.Language= inputDialog.language;
                define();
            }
            else
            {
                MessageBox.Show("Please select a language " + Environment.NewLine + "curent : " + textBox.Language.ToString());
            }
        }
    }
}
