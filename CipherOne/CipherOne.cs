using CryptoNET.Cipher.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNET.Cipher.One
{
    public class CipherOne : ICipher
    {
        private SubstitutionTable _SBox;

        public CipherOne(SubstitutionTable substitutionTable)
        {
            _SBox = substitutionTable;
        }

        public int Decrypt(int crypt, int key)
        {
            int k0 = (key >> sizeof(int));
            int k1 = (key & 0xf);

            int v = crypt ^ k1;
            int u = _SBox[v, Access.Invers];
            int m = u ^ k0;

            return m;
        }

        public int Encrypt(int message, int key)
        {
            int k0 = (key >> sizeof(int));
            int k1 = (key & 0xf);

            int u = message ^ k0;
            int v = _SBox[u];
            int c = v ^ k1;

            return c;
        }
    }
}
