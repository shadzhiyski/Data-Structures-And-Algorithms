namespace Sortable_Collection.Sorters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Sortable_Collection.Contracts;

    public class BucketSorter : ISorter<int>
    {
        public int Max { get; set; }

        public void Sort(List<int> collection)
        {
            // TODO
        }
    }
}
