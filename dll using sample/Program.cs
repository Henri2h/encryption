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
                Console.Write("Enter command : ");
                input = Console.ReadLine();
                if (input == "encrypt")
                {
                    Console.Write("Text : ");
                    string text = Console.ReadLine();
                    Encrypt(text);
                }
                else if (input == "encryptPlain")
                {
                    string plainText = File.ReadAllText(@"D:\plain.txt");
                    Console.WriteLine("Plain text : " + plainText);
                    Encrypt(plainText);
                }
                else if (input == "morse")
                {   //morse
                    Console.Write("Morse : ");

                    string plainMorse = Console.ReadLine();
                    plainMorse = crypt_dll_aplication.A_k.GetFormatedString(plainMorse);
                    Console.WriteLine("Formated string : " + plainMorse);
                    string morse = crypt_dll_aplication.A_k.EncodeInMorse(plainMorse);

                    //morse
                    File.WriteAllText(@"D:\morse.txt", morse);
                    File.WriteAllText(@"D:\plainMorse.txt", plainMorse);

                    Console.WriteLine(morse);
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (input == "annalise")
                {
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


                }
            }
        }

        public static void Encrypt(string plain)
        {
            plain = crypt_dll_aplication.A_k.GetFormatedString(plain);

            Console.WriteLine("Formated string : " + plain);

            Console.Write("Augment : ");
            string password = Console.ReadLine();
            int augment = int.Parse(password);

            string encrypted = crypt_dll_aplication.A_k.Crypt(plain, augment);
            Console.WriteLine("Encrypted : " + encrypted);
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
