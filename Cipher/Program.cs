using CryptoNET.Cipher.Attacks;
using CryptoNET.Cipher.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoNET.Cipher.One;
using System.IO;
using System.Globalization;
using CryptoNET.Cipher.Two;

namespace CryptoNET.Cipher.CipherApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Program program = new Program(args);
            program.Execute();
            
            Console.ReadKey();
        }

        private SubstitutionTable substitutionTable;
        private TextWriter writer;
        private CipherView view;
        private string action;
        private string[] arguments;

        public Program(string[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException();

            substitutionTable = new SubstitutionTable();
            writer = Console.Out;
            action = args[1];

            int version = int.Parse(args[0]);
            switch (version)
            {
                case 1:
                    view = new CipherView(writer, new CipherOne(substitutionTable), substitutionTable);
                    break;
                case 2:
                    view = new CipherView(writer, new CipherTwo(substitutionTable), substitutionTable);
                    break;
                default:
                    throw new NotImplementedException("Version " + version + " not implemented!");
            }

            arguments = args.Skip(2).ToArray();
        }

        public void Execute()
        {
            switch (action)
            {
                case "e":
                    Encrypt();
                    break;
                case "d":
                    Decrypt();
                    break;
                case "lat":
                    view.PrintLinearApproximationTable();
                    break;
                case "sub":
                    view.PrintSubstitutionTable();
                    break;
                default:
                    throw new NotSupportedException("Action " + action);
            }
        }

        private void Encrypt()
        {
            if (arguments.Length != 2)
                throw new ArgumentException(nameof(arguments));

            int message = int.Parse(arguments[0], NumberStyles.HexNumber);
            int key = int.Parse(arguments[1], NumberStyles.HexNumber);
            view.Encrypt(message, key);
        }

        private void Decrypt()
        {
            if (arguments.Length != 4)
                throw new ArgumentException(nameof(arguments));

            int cipherText = int.Parse(arguments[0], NumberStyles.HexNumber);
            int key = int.Parse(arguments[1], NumberStyles.HexNumber);
            view.Decrypt(cipherText, key);
        }
    }
}
