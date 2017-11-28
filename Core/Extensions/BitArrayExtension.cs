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

        public static void SetAll(this BitArray bitArray, int value)
        {
            for (int i = 0; i < bitArray.Length; i++)
            {
                bitArray.Set(i, ((value >> i) & 1) == 1);
            }
        }
    }
}
