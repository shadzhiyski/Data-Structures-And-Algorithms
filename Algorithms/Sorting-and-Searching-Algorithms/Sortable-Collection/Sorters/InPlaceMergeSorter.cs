namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class InPlaceMergeSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            throw new NotImplementedException();
        }

        private void MergeSort(IList<T> array, T[] tempArray, int start, int end)
        {
            if (start <= end)
            {
                int middle = (end - start) / 2;
                T[] left = new T[middle];
                //CopyElementsTo(array, left, start);
                T[] right = new T[middle + (end - start) % 2];
                //CopyElementsTo(array, right, start + middle);

                if (middle > 1)
                {
                    MergeSort(array, left, start, start + middle);
                    MergeSort(array, right, start + middle, end);
                }

                Merge(array, start, start, start + middle, middle, end - middle);

                int leftMinIndex = start;
                int tempIndex = 0;

                //while (tempIndex < tempArray.Length && leftMinIndex <= end)
                //{
                //    array[leftMinIndex] = tempArray[tempIndex];
                //    leftMinIndex++;
                //    tempIndex++;
                //}
            }
        }

        private void Swap(ref List<T> array, int index1, int index2)
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

        private void Merge(IList<T> array, int index, int startLeft, int startRight, int leftLen, int rightLen)
        {
            //if (left.Length == 2)
            //{
            //    if (left[0].CompareTo(left[1]) > 0)
            //    {
            //        Swap(ref left, 0, 1);
            //    }
            //}

            //if (right.Length == 2)
            //{
            //    if (right[0].CompareTo(right[1]) > 0)
            //    {
            //        Swap(ref right, 0, 1);
            //    }
            //}

            while (leftLen - startLeft > 0 && leftLen - startRight > 0)
            {
                if (array[startLeft].CompareTo(array[startRight]) < 0)
                {
                    array[index++] = array[startLeft++];
                }
                else
                {
                    array[index++] = array[startRight++];
                }
            }

            if (startLeft < leftLen)
            {
                for (int i = startLeft; i < array.Count; i++)
                {
                    array[index++] = array[i];
                }
            }
            if (startRight < array.Count)
            {
                for (int i = startRight; i < array.Count; i++)
                {
                    array[index++] = array[i];
                }
            }
        }
    }
}
