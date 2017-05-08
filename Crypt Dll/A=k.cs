using crypt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_dll_aplication
{

    public class A_k
    {
        public static List<Data> letters = new List<Data>();
        public static bool complexe = true;
        public static int total = 0;
        public static string letterSeparator = "/";
        public static string wordSeperator = "//";

        static char[] letter = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        static char[] letterMAJ = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        static char[] number = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        static string[] morseCode = { "._", "_...", "_._.", "_..", ".", ".._.", "__.", "....", "..", ".___", "_._" , "._..", "__", "_.", "___", ".__.", "__._", "._.", "...", "_", ".._", "..._", ".__","_.._", "_.__", "__..",
                    ".____","..___", "...__", "...._", ".....", "_....", "__...", "___..", "____.", "_____"
            };

        private static void Settings(int augment)
        {
            total = DefTotal(complexe);

            for (int i = 0; i < letter.Length; i++)
            {
                augment = AugmentDefine(i, augment);

                int encryptedPos = i + augment;
                if (encryptedPos > 25) { encryptedPos -= 26; }

                Data toAdd = new Data(letter[i], i, encryptedPos, morseCode[i]);
                letters.Add(toAdd);
            }

        }
        // crypt and decode
        public static string CryptCustom(string input_public, int augment)
        {
            bool first = true;

            StringBuilder sw = new StringBuilder();
            Settings(augment);

            foreach (char ch in input_public)
            {
                if (ch != ' ')
                {
                    if (!first)
                    {
                        sw.Append(letterSeparator);
                    }
                    int pos = FindLetterPos(ch);
                    if (pos != -1)
                    {
                        sw.Append(letters[pos].encryptValue);
                    }
                    first = false;

                }
                else
                {
                    sw.Append(wordSeperator);
                    first = true;
                }


            }
            return sw.ToString();
        }
        public static string DecodeCustom(string input_crypted, int augment)
        {
            // set the value of the letters
            Settings(augment);
            StringBuilder Out = new StringBuilder();
            using (StringReader input = new StringReader(input_crypted))
            {
                string line = "";
                while ((line = input.ReadLine()) != null)
                {
                    string[] words = line.Split(new[] { wordSeperator }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in words)
                    {
                        string[] chars = word.Split(new[] { letterSeparator }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string letter in chars)
                        {
                            int letterInt = Convert.ToInt32(letter);
                            int letterPos = FindEncryptedLetterPos(letterInt);
                            Out.Append(letters[letterPos].letter);
                        }
                        Out.Append(" ");
                    }
                    Out.AppendLine();
                }
            }
            return Out.ToString();
        }
        public static string Crypt(string input_public, int augment)
        {
            StringBuilder sw = new StringBuilder();
            Settings(augment);

            foreach (char ch in input_public)
            {
                if (ch != ' ')
                {
                    int pos = FindLetterPos(ch);

                    if (pos != -1)
                    {
                        sw.Append(letter[letters[pos].encryptValue]);
                    }
                }
                else
                {
                    sw.Append(' ');
                }
            }
            return sw.ToString();
        }

        // encode in morse
        public static string EncodeInMorse(string text)
        {
            Settings(0);

            StringBuilder builder = new StringBuilder();
            bool wasLetter = false;
            foreach (char ch in text)
            {
                if (ch != ' ')
                {
                    if (wasLetter == true) { builder.Append(letterSeparator); }
                    builder.Append(ReturnMorse(ch));
                    wasLetter = true;
                }
                else
                {
                    builder.Append(wordSeperator);
                    wasLetter = false;
                }
            }
            return builder.ToString();
        }

        public static string ReturnMorse(char letter)
        {
            int selectedLetterPos = FindLetterPos(letter);
            return letters[selectedLetterPos].morseCode;
        }

        public static string GetFormatedString(string text)
        {
            StringBuilder sb = new StringBuilder();
            StringReader sr = new StringReader(text);

            bool read = true;
            bool newLine = false;

            while (read)
            {
                string line = sr.ReadLine();
                if (line == null)
                {
                    read = false;
                    break;
                }
                if (newLine == true) { sb.AppendLine(); }

                foreach (char ch in line)
                {
                    if (letter.Contains(ch) || letterMAJ.Contains(ch))
                    {
                        char cha = char.ToLower(ch);
                        sb.Append(cha);
                    }
                    else if (ch == 'é' || ch == 'è')
                    {
                        sb.Append('e');
                    }
                    else if (ch == 'à')
                    {
                        sb.Append('a');
                    }
                    else if (ch == 'ç')
                    {
                        sb.Append('c');
                    }
                    else if (ch == 'ù')
                    {
                        sb.Append('u');
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }
                newLine = true;

            }

            return sb.ToString();
        }

        private static int AugmentDefine(int pos, int augment)
        {
            int total = DefTotal(complexe);
            int result = 0;
            if (pos < (total - augment))
            {
                result = pos + augment;
            }
            else
            {
                result = pos + augment - total;
            }
            return result;
        }
        private static int DefTotal(bool complexe)
        {
            int total = 26;
            if (complexe)
            {
                total = letters.ToArray().Length;
            }
            return total;
        }
        public static int FindLetterPos(char selectedLetter)
        {
            foreach (Data letter in letters)
            {
                if (letter.letter == selectedLetter)
                {
                    return letters.IndexOf(letter);
                }
            }
            return -1;
        }
        public static int FindEncryptedLetterPos(int letterCryptValue)
        {
            foreach (Data letter in letters)
            {
                if (letter.encryptValue == letterCryptValue)
                {
                    return letters.IndexOf(letter);
                }
            }
            return -1;
        }

    }
}
