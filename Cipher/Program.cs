using CryptoNET.Cipher.Attacks;
using CryptoNET.Cipher.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNET.Cipher.CipherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SubstitutionTable substitutionTable = new SubstitutionTable();
            
            var lat = new LinearApproximationTable(substitutionTable);
            var table = lat.CreateLinearApproximationTable();

            PrintTable(table);

            Console.ReadKey();
        }

        private static void PrintTable(Table table)
        {
            for (int i = 0; i <= 0xf; i++)
            {
                for (int j = 0; j <= 0xf; j++)
                {
                    Console.Write(table[i, j] + ", ");
                }
                Console.WriteLine();
            }

        }
    }
}
