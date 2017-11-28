using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNET.Cipher.Core.Extensions
{
    public static class BitArrayExtension
    {
        public static bool XorEach(this BitArray bitArray)
        {
            if (bitArray.Length == 0)
                return false;

            bool result = bitArray[0];

            for (int i = 1; i < bitArray.Length; i++)
            {
                result ^= bitArray[i];
            }

            return result;
        }
    }
}
