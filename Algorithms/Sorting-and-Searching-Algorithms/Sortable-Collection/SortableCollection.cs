namespace Sortable_Collection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sortable_Collection.Contracts;
    using System.Linq.Expressions;

    public class SortableCollection<T> where T : IComparable<T>
    {
        public SortableCollection()
        {
        }

        public SortableCollection(IEnumerable<T> items)
        {
            Items = new List<T>(items);
            Count = Items.Count;
        }

        public SortableCollection(params T[] items)
            : this(items.AsEnumerable())
        {
        }

        public List<T> Items = new List<T>();

        public int Count;// = Items.Count;

        public void Sort(ISorter<T> sorter)
        {
            sorter.Sort(this.Items);
        }

        public int BinarySearch(T item)
        {
            return BinarySearchLogic(item, 0, Items.Count - 1);
        }

        private int BinarySearchLogic(T item, int startIndex, int endIndex)
        {
            if (endIndex <= startIndex)
            {
                if (Items[startIndex].Equals(item)) { return startIndex; }
                return -1;
            }

            int midpoint = startIndex + (endIndex - startIndex) / 2;
            if (Items[midpoint].CompareTo(item) > 0)
            {
                return BinarySearchLogic(item, startIndex, midpoint);
            }
            else if (Items[midpoint].CompareTo(item) < 0)
            {
                return BinarySearchLogic(item, midpoint + 1, endIndex);
            }

            return midpoint;
        }

        public int InterpolationSearch(ref int[] sortedCollection, int item)
        {
            if (sortedCollection.Length == 0) { return -1; }
            // Returns index of toFind in sortedArray, or -1 if not found
            int low = 0;
            int high = sortedCollection.Length - 1;
            int mid;
            while (sortedCollection[low] <= item && sortedCollection[high] >= item)
            {
                mid = low + ((item - sortedCollection[low]) * (high - low)) /
                            (sortedCollection[high] - sortedCollection[low]);
                
                // out of range is possible here
                if (sortedCollection[mid] < item)
                    low = mid + 1;
                else if (sortedCollection[mid] > item)
                    high = mid - 1;
                else
                    return mid;
            }
            if (sortedCollection[low] == item) return low;
            else return -1; // Not found
        }

        public void Shuffle()
        {
            Random rnd = new Random();
            var n = Items.Count;
            for (var i = 0; i < n; i++)
            {
                // Exchange a[i] with random element in a[i … n-1]
                int r = i + rnd.Next(0, n - i);
                var temp = Items[i];
                Items[i] = Items[r];
                Items[r] = temp;
            }
        }

        public T[] ToArray()
        {
            return this.Items.ToArray();
        }

        public override string ToString()
        {
            return string.Join(" ", this.Items);
        }         
    }
}