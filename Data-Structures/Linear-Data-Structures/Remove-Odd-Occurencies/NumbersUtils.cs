namespace Remove_Odd_Occurencies
{
    using System;
    using System.Collections.Generic;

    public static class NumbersUtils
    {
        public static int FindMin(IEnumerable<int> numbers)
        {
            IEnumerator<int> enumerator = numbers.GetEnumerator();
            int min = 0;

            if (enumerator.MoveNext()) { min = enumerator.Current; }
            else { throw new Exception("No numbers given."); }

            while (enumerator.MoveNext())
            {
                if (enumerator.Current < min) { min = enumerator.Current; }
            }

            return min;
        }

        public static int FindMax(IEnumerable<int> numbers)
        {
            IEnumerator<int> enumerator = numbers.GetEnumerator();
            int max = 0;

            if (enumerator.MoveNext()) { max = enumerator.Current; }
            else { throw new Exception("No numbers given."); }

            while (enumerator.MoveNext())
            {
                if (enumerator.Current > max) { max = enumerator.Current; }
            }

            return max;
        }

        public static int[] FindOccurenciesInRange(IEnumerable<int> numbers, int minNumber, int maxNumber)
        {
            IEnumerator<int> enumerator = numbers.GetEnumerator();
            int[] occurencies = new int[maxNumber + 1 - minNumber];

            while (enumerator.MoveNext())
            {
                occurencies[enumerator.Current - minNumber]++;
            }

            return occurencies;
        }
    }
}
