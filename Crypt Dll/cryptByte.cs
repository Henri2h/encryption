using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt
{
    public class cryptByte
    {
        static int total = 256;

        public static byte[] crypt_code(byte[] input_public, byte[] augmentKey)
        {
            int pos = 0;
            List<Byte> output = new List<byte>();
            foreach (byte we in input_public)
            {
                int output_int = defNum(Convert.ToInt32(we), augmentKey[pos]);
                pos++;
                if (pos >= augmentKey.Length)
                {
                    pos = 0;
                }
                output.Add(Convert.ToByte(output_int));
            }
            byte[] result = output.ToArray();
            return result;
        }

        public static byte[] crypt_decode(byte[] input_public, byte[] augmentKey)
        {
            int pos = 0;
            List<byte> ch_byte = new List<byte>();
            foreach (byte byteInput in input_public)
            {
                int output_int = defNum_soustract(Convert.ToInt32(byteInput), augmentKey[pos]);
                pos++;
                if (pos >= augmentKey.Length)
                {
                    pos = 0;
                }
                ch_byte.Add(Convert.ToByte(output_int));
            }
            byte[] result = ch_byte.ToArray();

            // ended
            return result;
        }

        private static int defNum(int pos, int augment)
        {
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
        private static int defNum_soustract(int pos, int augment)
        {
            int result = 0;
            if (0 <= (pos - augment))
            {
                result = pos - augment;
            }
            else
            {
                result = pos - augment + total;
            }
            return result;
        }
    }
}
