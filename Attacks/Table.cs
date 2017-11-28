using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attacks
{
    public class Table
    {
        private int[][] values;

        public Table(int SizeX, int SizeY)
        {
            values = new int[0xf + 1][];
            for (int i = 0; i <= 0xf; i++)
            {
                values[i] = new int[0xf + 1];
            }
        }

        public void Increment(int i, int j)
        {
            values[i][j]++;
        }
    }
}
