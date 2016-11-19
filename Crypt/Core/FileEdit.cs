using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using crypt_dll_aplication;
using crypt;

namespace Code.Core
{
    /// <summary>
    /// This class is used to open, modify and close the tdx files
    /// </summary>
    class FileEdit
    {
        /// <summary>
        /// Open a file
        /// </summary>
        /// <returns></returns>
        public static bool Open()
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
                TextEditor.inputText.newTab(dlg.FileName);
                FileEdit.Load();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Load the file into the textbox of the current tab
        /// </summary>
        /// <returns></returns>
        public static bool Load()
        {

            if (File.Exists(TextEditor.current.fileName))
            {
                try
                {
                    Stream file = new MemoryStream();

                    // if crypt = file and toCrypt = true
                    // or load the file in clear
                    // the file has to bee loaded in clear in the file Memory Stream


                    if (TextEditor.current.CryptFileMethod == Method.CryptFileMethod.file && TextEditor.current.isCrypt == true)
                    {
                        // read all file
                        byte[] ByteInput = File.ReadAllBytes(TextEditor.current.fileName);
                        byte[] ByteOutput = new byte[ByteInput.Length];

                        if (TextEditor.current.CryptMethod == Method.CryptMethod.custom)
                        {
                            // charge the code class witch allow to crypt and decrypt the text
                            ByteOutput = cryptByte.crypt_decode(ByteInput, TextEditor.current.augmentKey.ToArray());
                        }
                        else if (TextEditor.current.CryptMethod == Method.CryptMethod.AES)
                        {
                            ByteOutput = AES.AESDecrypt(ByteInput, TextEditor.current.augmentKey.ToArray());
                        }
                        else if (TextEditor.current.CryptMethod == Method.CryptMethod.clear)
                        {
                            // Should not append
                            ByteInput = ByteOutput;
                        }
                        // save the decoded bytes in the memory stream
                        file = new MemoryStream(ByteOutput);
                    }
                    else
                    {
                        // open the file and save his contente in the memory stream
                        FileStream fileStream = new FileStream(TextEditor.current.fileName, FileMode.Open);
                        fileStream.CopyTo(file);
                        fileStream.Close();

                    }

                    //set the output
                    file.Position = 0;
                    string[] lines = readXml(file);


                    if (TextEditor.current.CryptFileMethod == Method.CryptFileMethod.content && TextEditor.current.isCrypt == true)
                    {
                        for (int i = 0; i < lines.Length; i++)
                        {
                            // =================================================
                            //                     De crypt
                            // =================================================


                            string[] stringBytes = lines[i].Split(' ');
                            List<int> intBytes = new List<int>();

                            foreach (string stringByte in stringBytes)
                            {
                                if ((stringByte != "") && (stringByte != null) && (stringByte != Environment.NewLine))
                                {
                                    try
                                    {
                                        intBytes.Add(Convert.ToInt32(stringByte));
                                    }
                                    catch
                                    {
                                        Debug.WriteLine("Error convertin : <" + stringByte + "> to int");
                                    }
                                }
                                else if (stringByte == Environment.NewLine)
                                {
                                    Debug.WriteLine("New line detected");
                                }
                            }


                            // covnert int to byte
                            List<byte> textBytes = new List<byte>();
                            foreach (int intByte in intBytes)
                            {
                                textBytes.Add(Convert.ToByte(intByte));
                            }


                            // intitalisating variables
                            byte[] ByteInput = textBytes.ToArray();
                            byte[] ByteOutput = new byte[ByteInput.Length];


                            // custom
                            if (TextEditor.current.CryptMethod == Method.CryptMethod.custom)
                            {
                                // charge the code class witch allow to crypt and decrypt the text
                                ByteOutput = cryptByte.crypt_decode(ByteInput, TextEditor.current.augmentKey.ToArray());
                            }
                            // AES
                            else if (TextEditor.current.CryptMethod == Method.CryptMethod.AES)
                            {
                                ByteOutput = AES.AESDecrypt(ByteInput, TextEditor.current.augmentKey.ToArray());
                            }
                            // if the cryptMethod is clear and toCrypt = true, should not append
                            else if (TextEditor.current.CryptMethod == Method.CryptMethod.clear)
                            {
                                MessageBox.Show("Something append, text is clear in the content, but text have to be crypt." + Environment.NewLine + "It's ok, just ignore it");

                            }

                            // save the output
                            lines[i] = Encoding.UTF8.GetString(ByteOutput);
                        }
                    }
                    else
                    {
                        // if the content is not encrypted
                        // nothing to do
                    }
                    StringBuilder sb = new StringBuilder();
                    foreach (string input in lines)
                    {
                        sb.AppendLine(input);
                    }

                    TextEditor.current.UITbText.Text = sb.ToString();

                    // this text is a xmlFile
                    TextEditor.current.isXml = true;
                }

                // if something append
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    TextEditor.current.UITbText.Text = File.ReadAllText(TextEditor.current.fileName, Encoding.UTF8);
                    TextEditor.current.isXml = false;
                    System.Diagnostics.Debug.WriteLine(e);
                }

                // define the values
                TextEditor.current.saved = true;
                TextEditor.current.isModified = false;

                // rebuild the user interface
                TextEditor.inputText.define();
            }
            else
            {
                //   TextEditor.current.textbox.Text = "File doesn't exist";
                // save = false
                TextEditor.current.saved = false;
            }
            return true;
        }

        /// <summary>
        /// Updated
        /// </summary>
        /// <returns></returns>
        public static bool Save()
        { // this will be saved at the end 
            byte[] outputByte = null;

            if (TextEditor.current.fileName == "" || TextEditor.current.fileName == null || TextEditor.current.fileName == "New File")
            {
                bool result = chooseFileName();
                if (result) { Save(); }
            }
            else
            {
                // let's save the file
                TextEditor.current.isXml = true;
                if (TextEditor.current.isXml == true)
                {
                    MemoryStream file = new MemoryStream();
                    StreamWriter outputString = new StreamWriter(file, Encoding.UTF8);


                    //create the document into a xml form

                    XmlDocument doc = new XmlDocument();

                    // root
                    XmlDeclaration XmlDeclaration = doc.CreateXmlDeclaration("1.0", "utf-8", "no");
                    XmlElement root = doc.DocumentElement;
                    doc.InsertBefore(XmlDeclaration, root);

                    // first stage
                    XmlElement document = doc.CreateElement(string.Empty, "Documents", string.Empty);
                    doc.AppendChild(document);


                    using (StringReader sr = new StringReader(TextEditor.current.UITbText.Text))
                    {
                        string line = "";
                        while ((line = sr.ReadLine()) != null)
                        {
                            // if the CryptFileMethod is set to file, nothing append because the text is saved in clear inside the file
                            // so we use the line variable without changing it
                            if (TextEditor.current.CryptFileMethod == Method.CryptFileMethod.content)
                            {
                                // =========================================
                                //            Crypt content
                                // =========================================

                                // intitialisation of the input and input
                                byte[] ByteInput = Encoding.UTF8.GetBytes(line);
                                byte[] ByteOutput = new byte[ByteInput.Length];

                                // selecting the ouput
                                // custom
                                if (TextEditor.current.CryptMethod == Method.CryptMethod.custom)
                                {

                                    ByteOutput = cryptByte.crypt_code(ByteInput, TextEditor.current.augmentKey.ToArray());
                                }
                                // AES
                                else if (TextEditor.current.CryptMethod == Method.CryptMethod.AES)
                                {
                                    ByteOutput = AES.AESEncrypt(ByteInput, TextEditor.current.augmentKey.ToArray());
                                }
                                // clear, should not append
                                else if (TextEditor.current.CryptMethod == Method.CryptMethod.clear)
                                {
                                    // almost nothing to change here
                                    // because we use at the end the line variable and he is not changed
                                }

                                if (TextEditor.current.CryptMethod != Method.CryptMethod.clear)
                                {
                                    StringBuilder sw = new StringBuilder();
                                    foreach (byte we in ByteOutput)
                                    {
                                        sw.Append(Convert.ToInt32(we));
                                        sw.Append(" ");
                                    }
                                    line = sw.ToString();
                                }
                            }


                            XmlElement text = doc.CreateElement(string.Empty, "Text", string.Empty);
                            XmlText textValue = doc.CreateTextNode(line);
                            text.AppendChild(textValue);
                            document.AppendChild(text);
                        }

                        // release the string reader, we have stored all the text in the Xml Doxument
                    }

                    doc.Save(outputString);
                    file.Position = 0;


                    // handle different types of encryption

                    if (TextEditor.current.CryptFileMethod == Method.CryptFileMethod.file)
                    {
                        byte[] ByteInput = Encoding.UTF8.GetBytes(TextEditor.current.UITbText.ToString());

                        // custom
                        if (TextEditor.current.CryptMethod == Method.CryptMethod.custom)
                        {
                            outputByte = cryptByte.crypt_code(ByteInput, TextEditor.current.augmentKey.ToArray());
                        }
                        // AES
                        else if (TextEditor.current.CryptMethod == Method.CryptMethod.AES)
                        {
                            outputByte = AES.AESEncrypt(ByteInput, TextEditor.current.augmentKey.ToArray());
                        }
                        // if the text is clear
                        // should not append
                        else if (TextEditor.current.CryptMethod == Method.CryptMethod.clear)
                        {
                            outputByte = file.ToArray();
                        }

                    }
                    else
                    {
                        //if cryptfilemethod = content
                        outputByte = file.ToArray();
                    }

                }
                else
                {
                    // if the file isn't xml, it is save in plain text
                    //convert the textbox content in byte[]
                    outputByte = Encoding.UTF8.GetBytes(TextEditor.current.UITbText.Text);
                }

                // save the file
                File.WriteAllBytes(TextEditor.current.fileName, outputByte);

                //set the output :
                TextEditor.current.saved = true;
                TextEditor.current.isModified = false;
                TextEditor.inputText.define();

                // exit the code, file save, ok
                return true;
            }
            // retrun false because if the code here is executed, it mean that the file hasn't been saved.
            return false;

        }

        /// <summary>
        /// Close the tab and check if it is needed to save the file
        /// </summary>
        /// <returns></returns>
        public static bool Close()
        {
            // if save == false or content == ""
            if ((TextEditor.current.saved == false) && (TextEditor.current.UITbText.Text != ""))
            {
                // debug
                System.Diagnostics.Debug.WriteLine("Ok, text is not void");
                // Message box
                MessageBoxResult output = MessageBox.Show("ok do you wan't to save before closing ? " + TextEditor.current.fileName, "Save and dlete file ?", MessageBoxButton.YesNoCancel);
                string output_string = output.ToString();

                // cancel the task
                if (output_string == "Cancel") { }
                else
                // if want to save before closing
                if (output_string == "Yes")
                {
                    //check if he can save the file and if thre, close the tab
                    if (Save())
                    {
                        TextEditor.inputText.define();
                        TextEditor.current.remove();
                    }
                }
                // if didn't want to save
                if (output_string == "No") { TextEditor.current.remove(); }
            }
            else
            {
                TextEditor.current.remove();
            }
            return true;
        }

        /// <summary>
        /// ask the user to choose a file
        /// </summary>
        /// <returns></returns>
        public static bool chooseFileName()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".tdx";
            dlg.Filter = "text dialog xample (*.tdx)|*.tdx|txt (*.txt)|*.txt|others file|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                // save the file name
                TextEditor.current.fileName = dlg.FileName;
                TextEditor.inputText.define();
                return true;
            }
            else
            {
                MessageBox.Show("Please select a file!");
                return false;
            }
        }

        private static string[] readXml(Stream file)
        {
            List<string> sb = new List<string>();

            // this method is not very clean but it work and can handle utf-8 parsing characters
            // TODO : replace this method to take count of the structure of the xml file
            file.Position = 0;
            using (XmlReader reader = XmlReader.Create(file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "Text":
                                if (reader.Read())
                                {
                                    sb.Add(reader.Value.ToString());

                                }
                                break;
                        }
                    }
                }
            }
            return sb.ToArray();
        }

    }
}
