using crypt_dll_aplication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dll_using_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            while (input != "exit")
            {
                input = Console.ReadLine();
                if (input == "encrypt") {
                    Encrypt();

                }
            }

            //morse
            Console.Write("Morse : ");
            string plainMorse = Console.ReadLine();
            plainMorse = crypt_dll_aplication.A_k.GetFormatedString(plainMorse);
            Console.WriteLine("Formated string : " + plainMorse);
            string morse = crypt_dll_aplication.A_k.EncodeInMorse(plainMorse);
            Console.WriteLine(morse);
            Console.ReadLine();
            Console.Clear();

            Console.Write("Annalise : ");

            // analising
            int augment = 10;
            string plain = Console.ReadLine();
            plain = crypt_dll_aplication.A_k.GetFormatedString(plain);

            Console.WriteLine("Formated string : " + plain);

            string encrypted = crypt_dll_aplication.A_k.CryptCustom(plain, augment);
            string decrypted = crypt_dll_aplication.A_k.DecodeCustom(encrypted, augment);

            Console.WriteLine("Encrypted : " + encrypted);
            Console.WriteLine("Decrypted : " + decrypted);

            //encrypted
            File.WriteAllText(@"D:\plain.txt", plain);
            File.WriteAllText(@"D:\encrypted.txt", encrypted);

            //morse
            File.WriteAllText(@"D:\morse.txt", morse);
            File.WriteAllText(@"D:\plainMorse.txt", plainMorse);

            Console.ReadKey();
        }

        public static void Encrypt()
        {
            Console.Write("Text : ");
            string plain = Console.ReadLine();
            plain = crypt_dll_aplication.A_k.GetFormatedString(plain);

            Console.WriteLine("Formated string : " + plain);

            Console.Write("Augment : ");
            string password = Console.ReadLine();
            int augment = int.Parse(password);

            string encrypted = crypt_dll_aplication.A_k.CryptCustom(plain, augment);

            File.WriteAllText(@"D:\encrypted.txt", encrypted);
        }

        public static void Analize(string text)
        {
            analiticValue[] frequency = crypt_dll_aplication.analitycs.analizeFrequency(text);

            foreach (analiticValue letter in frequency)
            {
                Console.WriteLine(letter.letter + " : " + letter.frequency);
            }
        }
    }
}
