using Fibonacci_Heap.Contracts;
using System;
using System.Collections.Generic;

public class FibonacciHeap<P, V> : IHeap<P, V> where P : IComparable<P>
{
	private const int DEFAULT_LEVEL_SIZE = 8;
    private DoublyLinkedList<Element> treeList;
	private DoublyLinkedListNode<Element> minElement;
	
	public FibonacciHeap()
	{
		treeList = new DoublyLinkedList<Element>();	
	}
	
	public int Count { get; private set; }
	
	public void Insert(P priority, V value)
	{
		Element element = new Element(priority, value);
        if (treeList.Count > 0)
        {
            AddBeforeMin(element);
        }
        else
        {
            treeList.Add(element);
            minElement = treeList.last;
        }
		
		Count++;
	}
	
	private void AddBeforeMin(Element element)
    {
        if (treeList.Count > 0)
        {
            if (!minElement.Equals(treeList.first))
            {
                var listNode = new DoublyLinkedListNode<Element>(element);
                minElement.Prev.Next = listNode;
                listNode.Prev = minElement.Prev;
                listNode.Next = minElement;
                minElement.Prev = listNode;
                treeList.Count++;
            }
            else
            {
                treeList.AddFirst(element);
            }
        }
        else
        {
            treeList.Add(element);
            minElement = treeList.first;
        }

        if (minElement.Value.Priority.CompareTo(element.Priority) > 0)
        {
            minElement = minElement.Prev;
        }
    }

    public V FindMin()
    {
        return minElement.Value.Value;
    }
	
	public V DeleteMin()
	{
		if (minElement == null) 
        {
            throw new InvalidOperationException("The heap is empty.");
        }

		var minValue = minElement.Value.Value;
		var children = minElement.Value.TreeList;
		AddListBeforeMin(children);
		RemoveMin();
        minElement = null;
        BuildTrees();
        CheckMin();

        Count--;
		return minValue;
	}
	
	private bool AddListBeforeMin(DoublyLinkedList<Element> list)
	{
		if (list.Count > 0)
		{
			list.first.Prev = minElement.Prev;
            if (minElement.Prev != null)
            {
                minElement.Prev.Next = list.first;
            }
            else
            {
                treeList.first = list.first;
            }
			list.last.Next = minElement;
            minElement.Prev = list.last;
			
			treeList.Count = treeList.Count + list.Count;
			return true;
		}
		
		return false;
	}
	
	private bool RemoveMin()
	{
        return RemoveNodeFromList(minElement);
	}

    private void BuildTrees()
    {
        int numberOfTrees = treeList.Count, count = 0;
        DoublyLinkedListNode<Element>[] levels = 
			new DoublyLinkedListNode<Element>[DEFAULT_LEVEL_SIZE]; 
        //IList<DoublyLinkedListNode<Element>> levels = new List<DoublyLinkedListNode<Element>>();
        var currNode = treeList.first;
        while (count < numberOfTrees)
        {
			bool hasTreesWithSameLevels = false;
			var node = currNode;
			RemoveNodeFromList(node);
            CheckCapacity(node.Value.TreeList.Count, ref levels);
            var levelNode = levels[node.Value.TreeList.Count];
            while (levelNode != null && node != null)
            {
				hasTreesWithSameLevels = true;
                if (levelNode.Value.Priority
						.CompareTo(node.Value.Priority) > 0)
                {
                    levels[node.Value.TreeList.Count] = null;
                    node.Value.TreeList.Add(levelNode.Value);
					CheckCapacity(node.Value.TreeList.Count, ref levels);
                    levelNode = levels[node.Value.TreeList.Count];
                }
                else
                {
					levels[node.Value.TreeList.Count] = null;
                    levelNode.Value.TreeList.Add(node.Value);
					node = levelNode;
					CheckCapacity(node.Value.TreeList.Count, ref levels);
                    levelNode = levels[node.Value.TreeList.Count];
                }
            }

            levels[node.Value.TreeList.Count] = node;
			currNode = currNode.Next;
            count++;
        }
		
		RebuildTreeList(levels);
    }
	
	private int FindNumberOfNodes(int numberOfChildren)
	{
        if (numberOfChildren > 0)
        {
            return (1 << (numberOfChildren - 1)) + 1;
        }

        return 1;
	}

    private void CheckCapacity(int index, ref DoublyLinkedListNode<Element>[] levels)
    {
        if (index >= DEFAULT_LEVEL_SIZE) { Reallocate(ref levels); }
    }

    private bool RebuildTreeList(DoublyLinkedListNode<Element>[] list)
	{
        treeList.Clear();
		if (list.Length > 0)
		{
            for (int i = list.Length - 1; i >= 0; i--)
			{
				if(list[i] != null)
				{
					treeList.Add(list[i].Value);
				}
			}

			return true;
		}
		
		return false;
	}

    private bool RemoveNodeFromList(DoublyLinkedListNode<Element> node)
    {
        if (node != null)
        {
            if (node.Prev != null)
            {
                node.Prev.Next = node.Next;
                if (node.Next != null)
                {
                    node.Next.Prev = node.Prev;
                }
                else
                {
                    treeList.last = node.Prev;
                }
            }
            else if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
                if (node.Prev != null)
                {
                    node.Prev.Next = node.Next;
                }
                else
                {
                    treeList.first = node.Next;
                }
            }
            else
            {
                treeList.first = treeList.last = null;
            }

            treeList.Count--;
            return true;
        }

        return false;
    }

    private void Reallocate(ref DoublyLinkedListNode<Element>[] nodesArray)
    {
        int size = nodesArray.Length;
        var newArray = new DoublyLinkedListNode<Element>[2 * size];
        for (int i = 0; i < size; i++)
        {
            newArray[i] = nodesArray[i];
        }

        nodesArray = newArray;
    }
	
	private void CheckMin()
    {
        int count = treeList.Count;
		var node = treeList.first;
		while(--count >= 0)
		{
			if(minElement != null)
			{
				if(minElement.Value.CompareTo(node.Value) > 0)
				{
					minElement = node;
				}
			}
			else
			{
				minElement = node;
			}

            node = node.Next;
		}
    }

    public void DecreaseKey(P priority, P newPriority)
    {
        throw new NotImplementedException(); // TO DO
    }

    public IHeap<P, V> Merge(IHeap<P, V> heap)
    {
        throw new NotImplementedException(); // TO DO
    }
	
	private class Element : IComparable<Element> 
	{
		public Element(P priority, V value)
		{
			Priority = priority;
			Value = value;
            TreeList = new DoublyLinkedList<Element>();
		}

        public DoublyLinkedList<Element> TreeList { get; private set; }
		
		public P Priority { get; set; }
		
		public V Value { get; set; }
		
		public int CompareTo(Element obj)
		{
			return Priority.CompareTo(obj.Priority);
		}
	}
}