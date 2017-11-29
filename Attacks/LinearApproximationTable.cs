using CryptoNET.Cipher.Core;
using CryptoNET.Cipher.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNET.Cipher.Attacks
{
    public class LinearApproximationTable
    {
        public SubstitutionTable SBox { get; protected set; }
        public Table Table { get; protected set; }

        public LinearApproximationTable(SubstitutionTable substitutionTable)
        {
            SBox = substitutionTable;
            Table = CreateLinearApproximationTable();
        }

        private Table CreateLinearApproximationTable()
        {
            Table result = new Table(16);

            for (int x_value = 0x0; x_value <= 0xf; x_value++)
            {
                int y_value = SBox[x_value];

                var x = CreateBitArray(x_value);
                var y = CreateBitArray(y_value);

                for (int a_value = 0x0; a_value <= 0xf; a_value++)
                {
                    var a = CreateBitArray(a_value);

                    for (int b_value = 0x0; b_value <= 0xf; b_value++)
                    {
                        var b = CreateBitArray(b_value);

                        var a_term = a.And(x).XorEach();  //bool a_term = a[3] & x[3] ^ a[2] & x[2] ^ a[1] & x[1] ^ a[0] & x[0];
                        var b_term = b.And(y).XorEach();  //bool b_term = b[3] & y[3] ^ b[2] & y[2] ^ b[1] & y[1] ^ b[0] & y[0];

                        if (a_term == b_term)
                        {
                            result[a_value, b_value]++;
                        }
                    }
                }
            }

            result.Normalize();
            return result;
        }

        public void Attack()
        {
            var entries = Table.GetSecondMaximumEntries();
            List<int> keys = new List<int>();
            foreach (var entry in entries)
            {
                var a = CreateBitArray(entry.Row);
                var b = CreateBitArray(entry.Column);

                bool lhs = a[1] ^ a[0] ^ b[2] ^ b[1] ^ b[0];

                for (int key = 0b00000000; key <= 0b11111111; key++)
                {
                    var k0 = CreateBitArray(key >> 4, 4);
                    var k1 = CreateBitArray(key & 0xf, 4);

                    bool rhs = k0[1] ^ k0[0] ^ k1[2] ^ k1[1] ^ k1[0];

                    //if (lhs == rhs) // Gleich nie erfüllt?! key=97 wird gefunden
                    //{
                        if (k0[0] ^ k1[2] != false)
                            continue;
                        if (k0[0] ^ k1[1] ^ k1[3] != false)
                            continue;
                        if (k0[1] ^ k1[1] ^ k1[2] != false)
                            continue;
                        if (k0[1] ^ k1[0] ^ k1[2] ^ k1[3] != false)
                            continue;
                        if (k0[0] ^ k0[1] ^ k1[0] ^ k1[1] ^ k1[2] != false)
                            continue;
                        if (k0[2] ^ k1[2] != true)
                            continue;
                        if (k0[2] ^ k1[0] ^ k1[2] ^ k1[3] != false)
                            continue;
                        if (k0[0] ^ k0[2] ^ k1[1] != false)
                            continue;
                        if (k0[0] ^ k0[2] ^ k1[0] ^ k1[2] != true)
                            continue;

                        int[] bytes = new int[2];
                        k0.CopyTo(bytes, 0);
                        k1.CopyTo(bytes, 1);
                        keys.Add((bytes[0] << 4) | bytes[1]);
                    //}

                }
            }
        }

        private BitArray CreateBitArray(int value, int size = 4)
        {
            var bitArray = new BitArray(size);
            bitArray.SetAll(value);
            return bitArray;
        }
    }
}
