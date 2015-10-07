namespace Permutations_With_Reps
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 1, 3, 5, 5 };
            
            PermuteRep(arr, 0, arr.Length - 1);
        }

        static void PermuteRep(int[] arr, int start, int end)
        {
            Print(arr);

            for (int left = end - 1; left >= start; left--)
            {
                for (int right = left + 1; right <= end; right++)
                {
                    if (arr[left] != arr[right])
                    {
                        Swap(ref arr[left], ref arr[right]);
                        PermuteRep(arr, left + 1, end);
                    }
                }

                // Undo all modifications done by the
                // previous recursive calls and swaps
                var firstElement = arr[left];
                for (int i = left; i <= end - 1; i++)
                {
                    arr[i] = arr[i + 1];
                }
                arr[end] = firstElement;
            }
        }

        static void Print<T>(T[] arr)
        {
            Console.WriteLine("{" + string.Join(", ", arr) + "}");
        }

        static void Swap<T>(ref T first, ref T second)
        {
            T oldFirst = first;
            first = second;
            second = oldFirst;
        }
    }
}
