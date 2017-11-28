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
        private SubstitutionTable _SBox;

        public LinearApproximationTable(SubstitutionTable substitutionTable)
        {
            _SBox = substitutionTable;
        }

        public Table CreateLinearApproximationTable()
        {
            Table result = new Table(16);

            for (int x_value = 0x0; x_value <= 0xf; x_value++)
            {
                int y_value = _SBox[x_value];

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

        private BitArray CreateBitArray(int value, int size = 4)
        {
            var bitArray = new BitArray(size);
            bitArray.SetAll(value);
            return bitArray;
        }
    }
}
