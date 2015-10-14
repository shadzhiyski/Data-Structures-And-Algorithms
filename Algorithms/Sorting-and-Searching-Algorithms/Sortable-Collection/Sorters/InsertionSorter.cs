namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;

    using Sortable_Collection.Contracts;

    public class InsertionSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(List<T> collection)
        {
            for (int i = 1; i < collection.Count; i++)
            {
                int targetIndex = i - 1;
                while (targetIndex >= 0 && collection[i].CompareTo(collection[targetIndex]) < 0) 
                {
                    targetIndex--;
                }

                if (++targetIndex < i)
                {
                    for (int j = i; j > targetIndex; j--)
                    {
                        Swap(ref collection, j, j - 1);
                    }
                }
            }
        }

        private void Swap(ref List<T> collection, int index1, int index2)
        {
            T temp = collection[index1];
            collection[index1] = collection[index2];
            collection[index2] = temp;
        }
    }
}
