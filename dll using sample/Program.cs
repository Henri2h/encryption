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
            string text = crypt_dll_aplication.A_k.encodeInMorse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(text);
            File.WriteAllText(@"D:\morse.txt", text);
            Console.ReadLine();
            string toAnnalize = File.ReadAllText(@"D:\text.txt");
            analiticValue[] frequency = crypt_dll_aplication.analitycs.analizeFrequency(toAnnalize);

            foreach (analiticValue letter in frequency)
            {
                Console.WriteLine(letter.letter + " : " + letter.frequency);
            }


            Console.Write("To encrypt : ");
            string plain = Console.ReadLine();
            Console.Write("Augment : ");
            int augment = Convert.ToInt32(Console.ReadLine());
            string encrypted = crypt_dll_aplication.A_k.cryptCustom(plain, augment);

            Console.WriteLine("Encrypted : " + encrypted);

            string decrypted = crypt_dll_aplication.A_k.decodeCustom(encrypted, augment);
            Console.WriteLine("Decrypted : " + decrypted);
            Console.ReadKey();
        }
    }
}
