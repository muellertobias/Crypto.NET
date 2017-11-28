using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNET.Cipher.Core
{
    public class Table
    {
        public int this[int indexX, int indexY]
        {
            get { return values[indexX][indexY]; }
            set { values[indexX][indexY] = value; }
        }

        private int[][] values;
        private bool _isNormalized;
        private int _size;

        public Table(int Size = 16)
        {
            _size = Size;
            _isNormalized = false;
            values = new int[_size][];
            for (int i = 0; i < _size; i++)
            {
                values[i] = new int[0xf + 1];
            }
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
                    values[i][j] -= factor;
                }
            }
            _isNormalized = true;
        }
    }
}
