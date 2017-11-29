using CryptoNET.Cipher.Core;
using CryptoNET.Cipher.Two;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNET.Cipher.Attacks.Two
{
    public static class CipherTwoAttacks
    {
        public static List<int> Attack(this CipherTwo two, int c1, int c2)
        {
            List<int> keys = new List<int>();
            Table ddt = two.GetDifferenceDistributionTable();

            // TODO search greatest differences and test theirs
            // ...
            int[] k2_values = new int[0x10];

            int key = 0x123;
            for (int m1 = 0x0; m1 <= 0xf; m1++)
            {
                int m2 = m1 ^ 0xf;
                c1 = two.Encrypt(m1, key);
                c2 = two.Encrypt(m2, key);
                for (int k2 = 0x0; k2 <= 0xf; k2++)
                {
                    int diff = two.SBox[c1 ^ k2, Access.Invers] ^ two.SBox[c2 ^ k2, Access.Invers];
                    if (diff == 0xd)
                    {
                        k2_values[k2]++;
                    }
                }
            }

            return keys;
        }

        public static Table GetDifferenceDistributionTable(this CipherTwo two)
        {
            int n = 16;
            Table table = new Table(n);

            for (int m1 = 0; m1 < n; m1++)
            {
                for (int m2 = 0; m2 < n; m2++)
                {
                    int d1 = m1 ^ m2;
                    int d2 = two.SBox[m1] ^ two.SBox[m2];
                    table[d1, d2]++;
                }
            }

            return table;
        }
    }
}
