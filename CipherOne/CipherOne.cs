using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherOne
{
    public class CipherOne : ICipher
    {
        public int Decrypt(int crypt, int key)
        {
            int k0 = (key >> sizeof(int));
            int k1 = (key & 0xf);

            int v = crypt ^ k1;
            int u = SubstitutionTable.InversSBox(v);
            int m = u ^ k0;

            return m;
        }

        public int Encrypt(int message, int key)
        {
            int k0 = (key >> sizeof(int));
            int k1 = (key & 0xf);

            int u = message ^ k0;
            int v = SubstitutionTable.SBox(u);
            int c = v ^ k1;

            return c;
        }
    }
}
