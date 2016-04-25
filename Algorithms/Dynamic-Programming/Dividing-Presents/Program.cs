using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dividing_Presents
{
    class Program
    {
        private static readonly uint NotSet = uint.MaxValue;

        static void Main(string[] args)
        {
            var presentsAmounts = ReadInput();
            Solve(presentsAmounts);
        }

        private static uint[] ReadInput()
        {
            return Console.ReadLine()
                .Split(',')
                .Select(p => uint.Parse(p))
                .ToArray();
        }

        private static void Solve(uint[] presentsValues)
        {
            // amount of all presents.
            uint presentsAmount = 0;
            
            // sum
            for (int i = 0; i < presentsValues.Length; i++)
            { presentsAmount += presentsValues[i]; }

            // last added present
            var lastPresent = new uint[presentsAmount * 2];
            for (int i = 1; i < lastPresent.Length; i++)
            {
                lastPresent[i] = NotSet;
            }
            
            // Finding all possible amounts of presents
            uint currSum = 0;
            for (uint i = 0; i < presentsValues.Length; i++)
            {
                for (uint j = presentsAmount; j + 1 > 0; j--)
                {
                    if (lastPresent[j] != NotSet 
                        && lastPresent[j + presentsValues[i]] == NotSet)
                    {
                        lastPresent[j + presentsValues[i]] = i;
                    }
                }

                currSum += presentsValues[i];
            }

            // Finding closest to presentsAmount/2 amount
            for (uint i = presentsAmount / 2; i > 1; i--)
            {
                if (lastPresent[i] != NotSet)
                {
                    Console.WriteLine("Difference: {0}", Math.Abs((int)i - (int)(presentsAmount - i)));
                    Console.WriteLine("{0}{1}{2}{3}", 
                        "Amount for Allan:", i, 
                        " Amount for Bob:", presentsAmount - i);
                    var firstPresents = new List<uint>();
                    while (i > 0)
                    {
                        firstPresents.Add(presentsValues[lastPresent[i]]);
                        i -= presentsValues[lastPresent[i]];
                    }

                    Console.WriteLine("Allan takes: {0}", string.Join(" ", firstPresents));
                    Console.WriteLine("Bob takes the rest.");
                    return;
                }
            }
        }
    }
}
