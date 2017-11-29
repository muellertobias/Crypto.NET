using CryptoNET.Cipher.Attacks;
using CryptoNET.Cipher.Core;
using CryptoNET.Cipher.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CryptoNET.Cipher.CipherApp
{
    public class CipherView
    {
        private readonly string format = "{3} 0x{0:x} -(0x{1:x})-> 0x{2:x}";

        private readonly TextWriter _writer;
        private readonly ICipher _cipher;
        private readonly SubstitutionTable _substitutionTable;

        public CipherView(TextWriter writer, ICipher cipher, SubstitutionTable substitutionTable)
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

            for (int i = 0; i <= 0xf; i++)
            {
                for (int j = 0; j <= 0xf; j++)
                {
                    _writer.Write(lat.Table[i, j] + ", ");
                }
                _writer.WriteLine();
            }
            lat.Attack();
            //var entries = lat.Table.GetSecondMaximumEntries();

            
            //foreach (var entry in entries)
            //{
            //    int counter = 0;
            //    var a = CreateBitArray(entry.Row);
            //    var b = CreateBitArray(entry.Column);

            //    bool lhs = a[1] ^ a[0] ^ b[2] ^ b[1] ^ b[0];

            //    for (int key = 0b00000; key <= 0b11111; key++)
            //    {
            //        var k = CreateBitArray(key, 5);
            //        bool rhs = k.XorEach();
            //        if (lhs == rhs)
            //        {
            //            counter++;
            //        }
            //    }

            //    _writer.WriteLine(string.Format("{0} von {1} Fällen getroffen", counter, 32));
            //}
        }

        private BitArray CreateBitArray(int value, int size = 4)
        {
            var bitArray = new BitArray(size);
            bitArray.SetAll(value);
            return bitArray;
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
