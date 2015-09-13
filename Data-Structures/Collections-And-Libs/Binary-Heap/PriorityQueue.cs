namespace Binary_Heap
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<TPriority, TValue>
    {
        private IList<KeyValuePair<TPriority, TValue>> binaryHeap;
        private IComparer<TPriority> comparer;

        public PriorityQueue()
            : this(Comparer<TPriority>.Default)
        {
        }

        public PriorityQueue(IComparer<TPriority> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("You must enter priority comparer.");

            binaryHeap = new List<KeyValuePair<TPriority, TValue>>();
            this.comparer = comparer;
        }

        public int Count
        {
            get { return binaryHeap.Count; }
        }

        public bool IsEmpty
        {
            get { return binaryHeap.Count == 0; }
        }

        public void Enqueue(TPriority priority, TValue value)
        {
            Insert(priority, value);
        }

        private void Insert(TPriority priority, TValue value)
        {
            KeyValuePair<TPriority, TValue> val =
                new KeyValuePair<TPriority, TValue>(priority, value);
            binaryHeap.Add(val);

            HeapifyFromLastToRoot(binaryHeap.Count - 1);
        }

        private int HeapifyFromLastToRoot(int pos)
        {
            if (pos >= binaryHeap.Count) return -1;

            while (pos > 0)
            {
                int parentPos = (pos - 1) / 2;
                if (comparer.Compare(binaryHeap[parentPos].Key, binaryHeap[pos].Key) > 0)
                {
                    Exchange(parentPos, pos);
                    pos = parentPos;
                }
                else break;
            }
            return pos;
        }

        private void Exchange(int pos1, int pos2)
        {
            KeyValuePair<TPriority, TValue> val = binaryHeap[pos1];
            binaryHeap[pos1] = binaryHeap[pos2];
            binaryHeap[pos2] = val;
        }

        public TValue Peek()
        {
            if (!IsEmpty)
                return binaryHeap[0].Value;
            else
                throw new InvalidOperationException("Priority queue is empty");
        }

        public TValue Dequeue()
        {
            if (!IsEmpty)
            {
                KeyValuePair<TPriority, TValue> result = binaryHeap[0];
                DeleteRoot();
                return result.Value;
            }

            throw new InvalidOperationException("Priority queue is empty");
        }

        private void DeleteRoot()
        {
            if (binaryHeap.Count <= 1)
            {
                binaryHeap.Clear();
                return;
            }

            binaryHeap[0] = binaryHeap[binaryHeap.Count - 1];
            binaryHeap.RemoveAt(binaryHeap.Count - 1);

            HeapifyFromRootToLast(0);
        }

        private void HeapifyFromRootToLast(int pos)
        {
            if (pos >= binaryHeap.Count) return;

            while (true)
            {
                int smallest = pos;
                int left = 2 * pos + 1;
                int right = 2 * pos + 2;
                if (left < binaryHeap.Count &&
                    comparer.Compare(binaryHeap[smallest].Key, binaryHeap[left].Key) > 0)
                    smallest = left;
                if (right < binaryHeap.Count &&
                    comparer.Compare(binaryHeap[smallest].Key, binaryHeap[right].Key) > 0)
                    smallest = right;

                if (smallest != pos)
                {
                    Exchange(smallest, pos);
                    pos = smallest;
                }
                else break;
            }
        }
    }
}
