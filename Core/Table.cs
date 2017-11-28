using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNET.Cipher.Core
{
    public class Entry
    {
        public int Value { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
    }

    public class Table
    {
        public int this[int indexX, int indexY]
        {
            get { return values[indexX, indexY]; }
            set { values[indexX, indexY] = value; }
        }

        private int[,] values;
        private bool _isNormalized;
        private int _size;

        public Table(int Size = 16)
        {
            _size = Size;
            _isNormalized = false;
            values = new int[_size, _size];
            values.Initialize();
        }

        public void Normalize()
        {
            if (_isNormalized)
                throw new ArithmeticException("Table is already normalized");

            int factor = _size >> 1;
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    this[i, j] -= factor;
                }
            }
            _isNormalized = true;
        }

        public List<Entry> GetSecondMaximumEntries()
        {
            List<Entry> entries = new List<Entry>();

            int maximum = -1;

            for (int a = 0; a < _size; a++)
            {
                for (int b = 0; b < _size; b++)
                {
                    if (a != 0 && b != 0)
                    {
                        if (maximum < values[a, b])
                        {
                            entries.Clear();
                            maximum = values[a, b];
                            entries.Add(new Entry() { Column = b, Row = a, Value = values[a, b] });
                        }
                        else if (maximum == values[a, b])
                        {
                            entries.Add(new Entry() { Column = b, Row = a, Value = values[a, b] });
                        }
                    }
                }
            }

            return entries;
        }
    }
}
