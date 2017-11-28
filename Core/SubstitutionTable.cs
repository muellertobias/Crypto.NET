using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNET.Cipher.Core
{
    public enum Access
    {
        Normal,
        Invers
    }

    public class SubstitutionTable
    {
        public int this[int index, Access access = Access.Normal]
        {
            get { return access != Access.Invers ? _table[index] : _table.First(kvp => kvp.Value == index).Key; }
        }

        private readonly Dictionary<int, int> _table;

        public SubstitutionTable()
        {
            _table = new Dictionary<int, int>
            {
                { 0x0, 0x6 },
                { 0x1, 0x4 },
                { 0x2, 0xc },
                { 0x3, 0x5 },
                { 0x4, 0x0 },
                { 0x5, 0x7 },
                { 0x6, 0x2 },
                { 0x7, 0xe },
                { 0x8, 0x1 },
                { 0x9, 0xf },
                { 0xa, 0x3 },
                { 0xb, 0xd },
                { 0xc, 0x8 },
                { 0xd, 0xa },
                { 0xe, 0x9 },
                { 0xf, 0xb }
            };
        }

        public SubstitutionTable(Dictionary<int, int> table)
        {
            _table = table;
        }
    }
}
