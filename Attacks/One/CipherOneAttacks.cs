using CryptoNET.Cipher.Core;
using CryptoNET.Cipher.One;
using System.Collections.Generic;

namespace CryptoNET.Cipher.Attacks.One
{
    public static class CipherOneAttacks 
    {
        public static List<int> Attack(this CipherOne one, int c1, int m1, int c2, int m2)
        {
            List<int> keys = new List<int>();

            for (int k0 = 0x0; k0 < 0xf; k0++)
            {
                int k1_1 = c1 ^ one.SBox[m1 ^ k0];
                int k1_2 = c2 ^ one.SBox[m2 ^ k0];

                if (k1_1 == k1_2)
                {
                    int key = k0 << sizeof(int) | k1_1;
                    int m1_test = one.Decrypt(c1, key);
                    if (m1_test == m1)
                    {
                        keys.Add(key);
                    }
                }
            }

            return keys;
        }
    }
}
