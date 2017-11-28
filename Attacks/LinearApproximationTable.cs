using CryptoNET.Cipher.Core;
using System;
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

            for (int x = 0x0; x <= 0xf; x++)
            {
                int x0 = x & 1;
                int x1 = (x >> 1) & 1;
                int x2 = (x >> 2) & 1;
                int x3 = (x >> 3) & 1;

                int y = _SBox[x];

                int y0 = y & 1;
                int y1 = (y >> 1) & 1;
                int y2 = (y >> 2) & 1;
                int y3 = (y >> 3) & 1;

                for (int a = 0x0; a <= 0xf; a++)
                {
                    int a0 = a & 1;
                    int a1 = (a >> 1) & 1;
                    int a2 = (a >> 2) & 1;
                    int a3 = (a >> 3) & 1;

                    for (int b = 0x0; b <= 0xf; b++)
                    {
                        int b0 = b & 1;
                        int b1 = (b >> 1) & 1;
                        int b2 = (b >> 2) & 1;
                        int b3 = (b >> 3) & 1;

                        int a_term = a3 & x3 ^ a2 & x2 ^ a1 & x1 ^ a0 & x0;
                        int b_term = b3 & y3 ^ b2 & y2 ^ b1 & y1 ^ b0 & y0;

                        if (a_term == b_term)
                        {
                            result[a, b]++;
                        }
                    }
                }
            }

            result.Normalize();
            return result;
        }
    }
}
