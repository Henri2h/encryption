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
            //morse
            Console.Write("Morse : ");
            string plainMorse = Console.ReadLine();
            string morse = crypt_dll_aplication.A_k.encodeInMorse(plainMorse);
            Console.WriteLine(morse);
            Console.ReadLine();
            Console.Clear();

            Console.Write("Annalise : ");

            // analising
            int augment = 10;
            string plain = Console.ReadLine();

            string encrypted = crypt_dll_aplication.A_k.cryptCustom(plain, augment);
            string decrypted = crypt_dll_aplication.A_k.decodeCustom(encrypted, augment);

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

        public static void analize(string text)
        {
            analiticValue[] frequency = crypt_dll_aplication.analitycs.analizeFrequency(text);

            foreach (analiticValue letter in frequency)
            {
                Console.WriteLine(letter.letter + " : " + letter.frequency);
            }
        }
    }
}
