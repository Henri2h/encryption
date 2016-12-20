using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt
{
    class wheel
    {
        int position = 0;
        public void changePosition(int ch)
        {
            position += ch;
            if (position > (letters.Length - 1)) { position -= letters.Length; }
            if (position < 0) { position += letters.Length; }
        }
        public void resetPos() { position = 0; }
        public char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    }
    class wheels
    {
        wheel[] wheelsList = new wheel[3];
        public void setWheels(int[] settings)
        {

        }
        public char getStringAfterEncoding()
        {

            return 'a';
        }
    }

    class Enigma
    {

    }
}
