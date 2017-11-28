using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SubstitutionTable
    {
        private static readonly Dictionary<int, int> _table;

        static SubstitutionTable()
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

        public static int SBox(int key)
        {
            return _table[key];
        }

        public static int InversSBox(int value)
        {
            var keyValuePair = _table.First(kvp => kvp.Value == value);
            return keyValuePair.Key;
        }
    }
}
