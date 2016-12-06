using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleHash
{
    class DoubleHash<Key, Value>
    {
        private MyData[] member;
        int size;
        int count;

        const double sizeFactor = 1.3;
        private class MyData
        {
            public Key key;
            public Value value;
            public Status status;

            public MyData(Key k, Value v)
            {
                key = k;
                value = v;
                status = Status.full;
            }
        }
        private enum Status
        {
            empty = 0,
            full = 1,
            deleted = -1
        }

        public DoubleHash(int size)
        {
            double tmpSize = size * sizeFactor;
            this.size = (int)tmpSize;
            member = new MyData[(int)tmpSize];
        }

        public bool Add(Key k, Value v)
        {
            int index = HashFunction(k);
            if (member[index] == null)
                member[index] = new MyData(k, v);
            if (member[index] != null)
            {
                switch (member[index].status)
                {
                    case Status.empty:
                    case Status.deleted:
                        member[index] = new MyData(k, v);
                        break;
                    case Status.full:
                        while ((member[index] != null) && (member[index].status == Status.full))
                        {
                            int tmpIndex = Jump(index);
                            index = tmpIndex;
                        }
                        member[index] = new MyData(k, v);
                        break;
                }
            }
            if (count == this.size)
                return false;
            count++;
            return true;
        }

        private int HashFunction(Key k)
        {
            string s = k.ToString();

            int h = 0, a = 31415, b = 27183;
            foreach (char c in s)
            {
                h = (a * h + c) % member.Length;
                a = a * b % (member.Length - 1);
            }
            return h;
        }

        private int Jump(int currIndex)
        {
            Random jumpRand = new Random();
            int tmpJump = jumpRand.Next(0, this.size);
            tmpJump %= member.Length; //To stay inside of the array index
            while (tmpJump == currIndex)
            {
                jumpRand.Next(0, this.size);
                tmpJump %= member.Length; //To stay inside of the array index
            }
            //string s = k.ToString();
            //int tmpJump = (int)s[1];
            //int charsNum = 0;
            //int tmpJump = 0;
            //for (int i = 0; i < s.Length; i++)
            //{
            //    tmpJump += s[i];
            //    charsNum++;
            //}
            //tmpJump /= charsNum; //AVG of the sum of the chars asci from the ToString()

            return tmpJump; 
        }


        private static bool IsPrime(int number)
        {
            if (number == 2 || number == 3)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            int divisor = 5;
            int step = 2;

            while (divisor * divisor <= number)
            {
                if (number % divisor == 0)
                    return false;

                divisor += step;
                step = 6 - step;
            }

            return true;
        }

        private static int GetNextPrime(int number)
        {
            if (number % 2 == 0 && number != 2)
            {
                number++;
            }

            while (!IsPrime(number))
                number += 2;

            return number;
        }


    }
}
