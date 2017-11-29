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
        private SubstitutionTable _SBox;

        public CipherTwo(SubstitutionTable substitutionTable)
        {
            _SBox = substitutionTable;
        }

        public int Decrypt(int crypt, int key)
        {
            int k0 = key >> 2 * sizeof(int) & 0xf;
            int k1 = key >> sizeof(int) & 0xf;
            int k2 = key & 0xf;

            int x = crypt ^ k2;
            int w = _SBox[x, Access.Invers];
            int v = w ^ k1;
            int u = _SBox[v, Access.Invers];
            int m = u ^ k0;

            return m;
        }

        public int Encrypt(int message, int key)
        {
            int k0 = key >> 2 * sizeof(int) & 0xf;
            int k1 = key >> sizeof(int) & 0xf;
            int k2 = key & 0xf;

            int u = message ^ k0;
            int v = _SBox[u];
            int w = v ^ k1;
            int x = _SBox[u];
            int c = x ^ k2;

            return c;
        }
    }
}
