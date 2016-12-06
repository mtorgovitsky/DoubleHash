using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleHash
{
    class Program
    {
        static void Main(string[] args)
        {
            DoubleHash<int, string> dhc = new DoubleHash<int, string>(10);
            Random rnd = new Random();

            int num;

            for (int i = 0; i < 30; i++)
            {
                num = rnd.Next();
                while (dhc.Add(num, num.ToString() + " ")) ;
            }
        }
    }
}
