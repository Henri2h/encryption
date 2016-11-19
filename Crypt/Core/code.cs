using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Code
{
    private class code
    {
        /// <summary>
        /// must provide methods to encrypt and decrypt nyte[] with a single paswword in byte[]
        /// </summary>

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



        // this salt should not exist, in the future, remove it
        private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

        public static byte[] AESEncrypt(byte[] plain, byte[] augmentKey)
        {
            string password = Encoding.UTF8.GetString(augmentKey);
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
            Encoding.UTF8.GetBytes(password);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plain, 0, plain.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        public static byte[] AESDecrypt(byte[] cipher, byte[] augmentKey)
        {
            string password = Encoding.UTF8.GetString(augmentKey);
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipher, 0, cipher.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
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
