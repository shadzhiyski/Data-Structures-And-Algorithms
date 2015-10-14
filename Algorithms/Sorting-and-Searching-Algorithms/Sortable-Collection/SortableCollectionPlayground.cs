namespace Sortable_Collection
{
    using System;
    using System.Linq;
    using Sortable_Collection.Sorters;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class MergeSortParallelExtension
    {
        public static void ParallelSort<T>(this List<T> collection)
            where T : IComparable<T>
        {
            MergeSortParallel(collection, new T[collection.Count], 0, collection.Count);
        }

        private static void MergeSortParallel<T>(List<T> array, T[] tempArray, int start, int end)
            where T : IComparable<T>
        {
            if (start <= end)
            {
                int middle = (end - start) / 2;
                bool isBigEnough = middle > 2 * Math.Log(array.Count, 2);
                T[] left = new T[middle];
                Task[] copyElementsToLeftTask = new Task[1];
                if (isBigEnough)
                {
                    copyElementsToLeftTask[0] = Task.Factory.StartNew(() => CopyElementsTo(array, left, start));
                }
                else 
                {
                    CopyElementsTo(array, left, start);
                }
                
                T[] right = new T[middle + (end - start) % 2];
                CopyElementsTo(array, right, start + middle);
                Task[] sortLeftTask = new Task[1];

                if (isBigEnough) { Task.WaitAll(copyElementsToLeftTask); }
                if (middle > 1)
                {
                    if (isBigEnough)
                    {
                        sortLeftTask[0] = Task.Factory.StartNew(() => MergeSortParallel(array, left, start, start + middle));
                    }
                    else
                    {
                        MergeSortParallel(array, left, start, start + middle);
                    }

                    MergeSortParallel(array, right, start + middle, end);
                    if (isBigEnough) { Task.WaitAll(sortLeftTask); }
                }

                Merge(ref array, left, right, start);
            }
        }

        private static void Swap<T>(ref T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        private static void CopyElementsTo<T>(IList<T> array, T[] temp, int start)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = array[i + start];
            }
        }

        private static void Merge<T>(ref List<T> array, T[] left, T[] right, int index)
            where T : IComparable<T>
        {
            int startRight = 0,
                startLeft = 0;

            if (left.Length == 2)
            {
                if (left[0].CompareTo(left[1]) > 0)
                {
                    Swap(ref left, 0, 1);
                }
            }

            if (right.Length == 2)
            {
                if (right[0].CompareTo(right[1]) > 0)
                {
                    Swap(ref right, 0, 1);
                }
            }

            while (startLeft < left.Length && startRight < right.Length)
            {
                if (left[startLeft].CompareTo(right[startRight]) < 0)
                {
                    array[index++] = left[startLeft++];
                }
                else
                {
                    array[index++] = right[startRight++];
                }
            }

            if (startLeft < left.Length)
            {
                for (int i = startLeft; i < left.Length; i++)
                {
                    array[index++] = left[i];
                }
            }
            if (startRight < right.Length)
            {
                for (int i = startRight; i < right.Length; i++)
                {
                    array[index++] = right[i];
                }
            }
        }
    }

    public static class SortableCollectionPlayground
    {
        private static Random Random = new Random();

        public static void Main(string[] args)
        {
            const int NumberOfElementsToSort = 2800000;
            const int MaxValue = 150;

            var array = new int[NumberOfElementsToSort];

            for (int i = 0; i < NumberOfElementsToSort; i++)
            {
                array[i] = Random.Next(MaxValue);
            }

            //var collectionToSort = new SortableCollection<int>(array);
            //collectionToSort.Sort(new BucketSorter { Max = MaxValue });

            //Console.WriteLine(collectionToSort);

            var collection = new SortableCollection<int>(array);//new int[] { 2, 2, 2, 2, 2, -1, 5, 0, -3, 14, 83 } );
            //Console.WriteLine(collection);
            Console.WriteLine();

            Console.WriteLine("Standard Merge Sort started...");
            var startConsecutiveRun = DateTime.Now;
            collection.Sort(new MergeSorter<int>());
            var endConsecutiveRun = DateTime.Now;
            //Console.WriteLine(collection);
            Console.WriteLine("Time: {0}", endConsecutiveRun - startConsecutiveRun);

            var elems = collection.ToArray();
            List<int> clonedCollection = elems.ToList();

            Console.WriteLine();
            Console.WriteLine("Parallel In-Place Merge Sort started...");
            var startParallelRun = DateTime.Now;
            clonedCollection.ParallelSort();
            var endParallelRun = DateTime.Now;
            //Console.WriteLine(string.Join(" ", clonedCollection));
            Console.WriteLine("Time: {0}", endParallelRun - startParallelRun);

            Console.WriteLine("Advantage: {0:0.00} %", 100 * ((1d - 
                (double)(endParallelRun - startParallelRun).Ticks / 
                (endConsecutiveRun - startConsecutiveRun).Ticks)));

            Console.WriteLine();
            Console.WriteLine("Standard Insertion started...");
            var startConsecutiveRunQ = DateTime.Now;
            collection.Sort(new InsertionSorter<int>());
            var endConsecutiveRunQ = DateTime.Now;
            //Console.WriteLine(collection);
            Console.WriteLine("Time: {0}", endConsecutiveRunQ - startConsecutiveRunQ);

            Console.WriteLine(AreEqual(collection.ToArray(), clonedCollection.ToArray()));
            //Console.WriteLine(collection.BinarySearch(2));
            //Console.WriteLine(collection.InterpolationSearch(ref elems, 2));
            //Console.WriteLine(Array.BinarySearch(elems, 2));
            //Console.WriteLine();
            //Console.WriteLine();

            //Console.WriteLine("Shuffling:");
            //for (int i = 0; i < 10; i++)
            //{
            //    collection.Shuffle();
            //    Console.WriteLine(string.Join(", ", collection));
            //}
        }

        private static bool AreEqual<T>(T[] array1, T[] array2)
        {
            for (int i = 0; i < array1.Length; i++)
            {
                if (!array1[i].Equals(array2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
