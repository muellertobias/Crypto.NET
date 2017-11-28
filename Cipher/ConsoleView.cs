using CryptoNET.Cipher.Attacks;
using CryptoNET.Cipher.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CryptoNET.Cipher.CipherApp
{
    public class ConsoleView
    {
        private readonly string format = "{3} 0x{0:x} -(0x{1:x})-> 0x{2:x}";

        private readonly TextWriter _writer;
        private readonly ICipher _cipher;
        private readonly SubstitutionTable _substitutionTable;

        public ConsoleView(TextWriter writer, ICipher cipher, SubstitutionTable substitutionTable)
        {
            _writer = writer;
            _cipher = cipher;
            _substitutionTable = substitutionTable;
        }

        public void Encrypt(int message, int key)
        {
            int cipherText = _cipher.Encrypt(message, key);
            _writer.WriteLine(string.Format(format, message, key, cipherText, nameof(Encrypt)));
        }

        public void Decrypt(int cipherText, int key)
        {
            int message = _cipher.Decrypt(cipherText, key);
            _writer.WriteLine(string.Format(format, cipherText, key, message, nameof(Decrypt)));
        }

        public void PrintLinearApproximationTable()
        {
            var lat = new LinearApproximationTable(_substitutionTable);
            var table = lat.CreateLinearApproximationTable();

            for (int i = 0; i <= 0xf; i++)
            {
                for (int j = 0; j <= 0xf; j++)
                {
                    _writer.Write(table[i, j] + ", ");
                }
                _writer.WriteLine();
            }
        }

        public void PrintSubstitutionTable()
        {
            for (int i = 0; i <= 0xf; i++)
            {
                _writer.WriteLine(string.Format("0x{0:x} -> 0x{1:x}", i, _substitutionTable[i]));
            }
        }
    }
}
