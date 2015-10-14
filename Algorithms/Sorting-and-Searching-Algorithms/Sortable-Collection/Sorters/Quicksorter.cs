namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class Quicksorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            Quicksort(collection, 0, collection.Count - 1);
        }

        private void Quicksort(List<T> array, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            T pivot = array[start];
            int storeIndex = start + 1;

            for (int i = start + 1; i <= end; i++)
            {
                if (array[i].CompareTo(pivot) < 0)
                {
                    Swap(i, storeIndex, ref array);
                    storeIndex++;
                }
            }

            storeIndex--;
            Swap(start, storeIndex, ref array);

            Quicksort(array, start, storeIndex);
            Quicksort(array, storeIndex + 1, end);
        }

        private void Swap(int i, int storeIndex, ref List<T> array)
        {
            T temp = array[i];
            array[i] = array[storeIndex];
            array[storeIndex] = temp;
        }
    }
}
