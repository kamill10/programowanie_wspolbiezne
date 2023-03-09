using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Solution1
{
    public class Calculator
    {
        public int pov(int a, int b)
        {
            int wynik = 1;
            for (int i = 0; i < b; i++)
            {
                wynik = wynik * a;
            }
            return wynik;
        }


    }

}