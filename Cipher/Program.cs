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

namespace CryptoNET.Cipher.CipherApp
{
    class Program
    {
        static int Main(string[] args)
        {
            int version = int.Parse(args[0]);
            string action = args[1];
            TextWriter writer = Console.Out;
            ConsoleView view = null;
            SubstitutionTable substitutionTable = new SubstitutionTable();

            switch (version)
            {
                case 1:
                    view = new ConsoleView(writer, new CipherOne(substitutionTable), substitutionTable);
                    break;
                default:
                    writer.WriteLine("Version " + version + " not implemented!");
                    return -1;
            }

            var code = DoAction(args, action, view);

            if (code == -1)
            {
                writer.WriteLine("Action " + action + " not implemented!");
            }
            else if (code == -2)
            {
                writer.WriteLine("Wrong arguments: {0}", args);
            }
            
            Console.ReadKey();
            return 0;
        }

        private static int DoAction(string[] args, string action, ConsoleView view)
        {
            switch (action)
            {
                case "e":
                    return Encrypt(view, args);
                case "d":
                    return Decrypt(view, args);
                case "lat":
                    return PrintLinearApproximationTable(view, args);
                case "sub":
                    return PrintLinearApproximationTable(view, args);
                default:
                    return -1;
            }
        }

        private static int Encrypt(ConsoleView view, string[] args)
        {
            if (args.Length != 4)
                return -2;

            int message = int.Parse(args[2], NumberStyles.HexNumber);
            int key = int.Parse(args[3], NumberStyles.HexNumber);
            view.Encrypt(message, key);

            return 0;
        }

        private static int Decrypt(ConsoleView view, string[] args)
        {
            if (args.Length != 4)
                return -2;

            int cipherText = int.Parse(args[2], NumberStyles.HexNumber);
            int key = int.Parse(args[3], NumberStyles.HexNumber);
            view.Decrypt(cipherText, key);

            return 0;
        }

        private static int PrintLinearApproximationTable(ConsoleView view, string[] args)
        {
            view.PrintLinearApproximationTable();
            return 0;
        }

        private static int PrintSubstitutionTable(ConsoleView view, string[] args)
        {
            view.PrintSubstitutionTable();
            return 0;
        }
    }
}
