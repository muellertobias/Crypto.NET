using CryptoNET.Cipher.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNET.Cipher.Two
{
    public class CipherTwo : ICipher
    {
        public SubstitutionTable SBox { get; protected set; }

        public CipherTwo(SubstitutionTable substitutionTable)
        {
            SBox = substitutionTable;
        }

        public int Decrypt(int crypt, int key)
        {
            int k0 = key >> 2 * sizeof(int) & 0xf;
            int k1 = key >> sizeof(int) & 0xf;
            int k2 = key & 0xf;

            int x = crypt ^ k2;
            int w = SBox[x, Access.Invers];
            int v = w ^ k1;
            int u = SBox[v, Access.Invers];
            int m = u ^ k0;

            return m;
        }

        public int Encrypt(int message, int key)
        {
            int k0 = key >> 2 * sizeof(int) & 0xf;
            int k1 = key >> sizeof(int) & 0xf;
            int k2 = key & 0xf;

            int u = message ^ k0;
            int v = SBox[u];
            int w = v ^ k1;
            int x = SBox[u];
            int c = x ^ k2;

            return c;
        }
    }
}
