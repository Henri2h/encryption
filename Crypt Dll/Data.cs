using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt
{
    public class Data
    {
        public char letter;
        public int Value;
        public int encryptValue = 0;
        public string morseCode = "";
        public int frequency = 0;
        public int total = 0;

        public Data(char letterValue)
        {
            letter = letterValue;
        }
        public Data(char letterValue, int pos)
        {
            letter = letterValue;
            Value = pos;
            encryptValue = pos;
        }
        public Data(char letterValue, int pos, int encryptpos)
        {
            letter = letterValue;
            Value = pos;
            encryptValue = encryptpos;
        }
        public Data(char letterValue, int pos, int encryptpos, string morse)
        {
            letter = letterValue;
            Value = pos;
            encryptValue = encryptpos;
            morseCode = morse;
        }

        // return the different value of Data
        public override string ToString()
        {
            StringBuilder SW = new StringBuilder();
            SW.Append("Letter : " + letter + " pos : " + Value + " encrypt value : " + encryptValue);
            return SW.ToString();
        }
    }
}
