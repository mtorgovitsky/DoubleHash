using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleHash
{
    class Program
    {
        static void Main(string[] args)
        {
            DoubleHash<int, string> dhc = new DoubleHash<int, string>(100);
            Random rnd = new Random();

            int num;

            for (int i = 0; i < 100; i++)
            {
                num = rnd.Next();
                dhc.Add(num, num.ToString() + " ") ;
                Debug.WriteLine(i);
            }
        }
    }
}
