namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Sortable_Collection.Contracts;
    using System.Threading.Tasks;

    public class MergeSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            MergeSort(collection, new T[collection.Count], 0, collection.Count);
        }

        private void MergeSort(IList<T> array, T[] tempArray, int start, int end) 
        {
            if (start <= end)
            {
                int middle = (end - start) / 2;
                T[] left = new T[middle];
                CopyElementsTo(array, left, start);
                T[] right = new T[middle + (end - start) % 2];
                CopyElementsTo(array, right, start + middle);

                if (middle > 1)
                {
                    MergeSort(array, left, start, start + middle);
                    MergeSort(array, right, start + middle, end);
                }

                Merge(tempArray, left, right);

                int leftMinIndex = start;
                int tempIndex = 0;

                while (tempIndex < tempArray.Length && leftMinIndex <= end)
                {
                    array[leftMinIndex] = tempArray[tempIndex];
                    leftMinIndex++;
                    tempIndex++;
                }
            }
        }

        private void Swap(ref T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        private void CopyElementsTo(IList<T> array, T[] temp, int start)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = array[i + start];
            }
        }

        private void Merge(T[] array, T[] left, T[] right)
        {
            int startRight = 0, 
                startLeft = 0, 
                index = 0;

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
}
