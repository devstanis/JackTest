using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackTest
{
    static class ChanceCalculator
    {

        // a номеров из n
        public static long GetCombonationCount(int a, int n)
        {
            long n1 = 1;
            for (int i = n; i > n - a; i--)
                n1 *= i;
            long n2 = GetFact(a);
            return n1/n2;
        }

        private static long GetFact(int num)
        {
            long res = 1;
            for (var i = 2; i <= num; i++)
                res *= i;
            return res;
        }
    }
}
