using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace File_encryption
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine("   starting file encryption");
            Console.WriteLine("   Made by Henri2h");
            Console.WriteLine("=================================");
            Console.WriteLine();

            // variables
            string inputFile = "";
            string outputFile = "";
            string mode = "";
            string inputkey = "";

            //loading startup details if launched wiht "open with ..."
            string[] activationData = Environment.GetCommandLineArgs();
            try
            {
                //enter parameters
                if (activationData[1] != null)
                {
                    inputFile = activationData[1];
                    outputFile = activationData[1];
                    Console.Write("mode : ");
                    mode = Console.ReadLine();

                    Console.Write("key : ");
                    inputkey = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("error");
                }
            }
            catch
            {
                // ================= load parameters from config file ==================


                string setupFile = System.Environment.CurrentDirectory + @"\setup\setup.txt";
                if (checkFileExist("setup file exist", setupFile))
                {
                    string[] config = File.ReadAllLines(setupFile);
                    mode = "";
                    if (config[0] == "mode=crypt")
                    {
                        mode = "crypt";
                    }
                    else if (config[0] == "mode=decrypt")
                    {
                        mode = "decrypt";
                    }
                    else if (config[0] == "create")
                    {
                        Console.WriteLine("Creating config file ...");
                        StringBuilder configfile = new StringBuilder();

                        configfile.AppendLine("<mode> can be \"crypt\" or \"decrypt\"");
                        configfile.AppendLine("inputKey in plain text");
                        configfile.AppendLine("<path to the input file>");
                        configfile.AppendLine("<path to the output file>");
                        configfile.AppendLine();
                        configfile.AppendLine("To show a sample file for encryption : write at the beginning of this file : \"create crypt\"");
                        configfile.AppendLine("or for a decryption : \"create decrypt\"");

                        File.WriteAllText(setupFile, configfile.ToString());
                        goto end;
                    }
                    else if (config[0] == "create crypt")
                    {
                        Console.WriteLine("Creating config file for encryption ...");
                        StringBuilder configfile = new StringBuilder();

                        configfile.AppendLine("mode=crypt");
                        configfile.AppendLine("password");
                        configfile.AppendLine(@"D:\inputfile.txt");
                        configfile.AppendLine(@"D:\outputfile.txt");

                        File.WriteAllText(setupFile, configfile.ToString());
                        goto end;
                    }
                    else if (config[0] == "create decrypt")
                    {
                        Console.WriteLine("Creating config file for encryption ...");
                        StringBuilder configfile = new StringBuilder();

                        configfile.AppendLine("mode=decrypt");
                        configfile.AppendLine("password");
                        configfile.AppendLine(@"D:\inputfile.txt");
                        configfile.AppendLine(@"D:\outputfile.txt");

                        File.WriteAllText(setupFile, configfile.ToString());
                        goto end;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bad parameters : write \"create\" to show config file");
                        Console.WriteLine("Path to the setup file : " + setupFile);
                        Console.ForegroundColor = ConsoleColor.White;
                        goto end;
                    }

                    // key
                    UTF8Encoding uni = new UTF8Encoding();
                    inputkey = config[1];

                    //input file
                    inputFile = config[2];

                    //output file
                    outputFile = config[3];
                }

                //mode

                else
                {

                    // console mode


                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Setup file hasn't been found in : " + setupFile);
                    Console.ForegroundColor = ConsoleColor.White;


                    Console.WriteLine("enter parameters manualy");
                    Console.WriteLine("you can create it by creating a file called setup.txt in a subdirectory of the exe file called setup : <exe directory>\\setup\\setup.exe");
                    Console.WriteLine("Type \"create\" in it to create the config file");

                    Console.WriteLine();

                    Console.Write("Input file : ");
                    inputFile = Console.ReadLine();

                    Console.Write("Output file : ");
                    outputFile = Console.ReadLine();

                    Console.Write("mode : ");
                    mode = Console.ReadLine();

                    Console.Write("key : ");
                    inputkey = Console.ReadLine();
                }



            }
            try
            {
                byte[] input = File.ReadAllBytes(inputFile);
                //=============== start =====================
                Console.WriteLine("started");
                //mode
                Console.WriteLine("Mode = " + mode);
                Console.WriteLine("Key = " + "***************");
                Console.WriteLine("input file = " + inputFile);
                Console.WriteLine("ouput file = " + outputFile);

                // if ecnrypt
                if (mode == "crypt")
                {
                    Console.WriteLine("starting crypt");
                    //encrypt
                    byte[] outputencrypt = Encrypt(input, inputkey);
                    File.WriteAllBytes(outputFile, outputencrypt);
                }
                // if decrypt
                else if (mode == "decrypt")
                {
                    Console.WriteLine("starting decoding");
                    byte[] result = Decrypt(File.ReadAllBytes(inputFile), inputkey);
                    File.WriteAllBytes(outputFile, result);
                }
            }
            catch
            {
                Console.WriteLine("Something went wrong .....");
                Console.WriteLine("Checking files ...");
                if (checkFileExist("Input file", inputFile) == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please be sure to have the good path : input path = " + inputFile);
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            end:
            //end
            Console.WriteLine("ended");
            Console.ReadKey();
        }

        /// <summary>
        /// Check if a file exist and display the result
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool checkFileExist(string name, string path)
        {
            bool result = false;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(name + " : [ ");
            if (File.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("OK");
                result = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("KO");
                result = false;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ]");
            return result;
        }

        private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

        public static byte[] Encrypt(byte[] plain, string password)
        {
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

        public static byte[] Decrypt(byte[] cipher, string password)
        {
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
        public static void schowresult(byte[] result)
        {
            StringBuilder resultsw = new StringBuilder();
            foreach (byte we in result)
            {
                resultsw.Append(Convert.ToInt32(we));
                resultsw.Append(" ");
            }
            Console.WriteLine(resultsw.ToString());
        }
    }

}
