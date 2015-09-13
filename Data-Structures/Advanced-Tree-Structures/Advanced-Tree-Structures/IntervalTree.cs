using System;
using System.Collections.Generic;

public class IntervalTree<TLimit> where TLimit : IComparable<TLimit>
{
    public class Interval : IComparable<Interval>
	{
		public Interval(TLimit start, TLimit end) 
		{
			if(start.CompareTo(end) > 0) 
			{
				throw new ArgumentException();
			}
			
			Start = start;
			End = end;
		}
		
		public TLimit Start { get; set; }
		
		public TLimit End { get; set; }
		
		public int CompareTo(Interval obj)
		{
			if(End.CompareTo(obj.End) > 0) { return 1; }
			else if(End.CompareTo(obj.End) < 0) { return -1; }
			else 
			{
				if(Start.CompareTo(obj.Start) > 0) { return 1; }
				else if(Start.CompareTo(obj.Start) < 0) { return -1; }
			}
			
			return 0;
		}
	}
	
    private class Node
    {
        // Node internal data
        internal int level;
        internal Node left;
        internal Node right;

        // User data
		internal Interval interval;
        internal TLimit value;

        // Constuctor for the sentinel node
        internal Node()
        {
            this.level = 0;
            this.left = this;
            this.right = this;
        }

        // Constuctor for regular nodes (that all start life as leaf nodes)
        internal Node(TLimit start, TLimit end, Node sentinel)
        {
            this.level = 1;
            this.left = sentinel;
            this.right = sentinel;
            interval = new Interval(start, end);
			interval.End = end;
            this.value = end;
        }
    }

    Node root;
    Node sentinel;
    Node deleted;

    private Node Root
    {
        get { return this.root; }
    }

    public IntervalTree()
    {
        root = sentinel = new Node();
        deleted = null;
    }

    private void Skew(ref Node node)
    {
        if (node.level == node.left.level)
        {
            // Rotate right
            Node left = node.left;
            node.left = left.right;
            left.right = node;
            node = left;
        }
		// set max val
		if(node.right != sentinel) 
		{ 
			node.value = node.right.value; 
		}
		else 
		{
			node.value = node.interval.End;
		}
    }

    private void Split(ref Node node)
    {
        if (node.right.right.level == node.level)
        {
            // Rotate left
            Node right = node.right;
            node.right = right.left;
            right.left = node;
            node = right;
            node.level++;
			// set max val
			if(node.left.right != sentinel) 
			{ 
				node.left.value = node.left.right.value; 
			}
			else 
			{
				node.left.value = node.left.interval.End;
			}
        }
		else 
		{
			// set max val
			if(node.right != sentinel) 
			{ 
				node.value = node.right.value;  
			}
			else 
			{
				node.value = node.interval.End;
			}
		}
    }

    private bool Insert(ref Node node, TLimit start, TLimit end)
    {
        if (node == sentinel)
        {
            node = new Node(start, end, sentinel);
            return true;
        }
		
		Interval interval = new Interval(start, end);
        int compare = interval.CompareTo(node.interval);
        if (compare < 0)
        {
            if (!Insert(ref node.left, start, end))
            {
                return false;
            }
        }
        else if (compare > 0)
        {
            if (!Insert(ref node.right, start, end))
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        Skew(ref node);
        Split(ref node);

        return true;
    }

    private bool Delete(ref Node node, TLimit start, TLimit end)
    {
        if (node == sentinel)
        {
            return (deleted != null);
        }

        Interval interval = new Interval(start, end);
        int compare = interval.CompareTo(node.interval);
        if (compare < 0)
        {
            if (!Delete(ref node.left, start, end))
            {
                return false;
            }
        }
        else
        {
            if (compare == 0)
            {
                deleted = node;
            }
            if (!Delete(ref node.right, start, end))
            {
                return false;
            }
        }

        if (deleted != null)
        {
            deleted.interval = node.interval;
            deleted = null;
            node = node.right;
        }
        else if (node.left.level < node.level - 1
                || node.right.level < node.level - 1)
        {
            --node.level;
            if (node.right.level > node.level)
            {
                node.right.level = node.level;
            }
            Skew(ref node);
            Skew(ref node.right);
            Skew(ref node.right.right);
            Split(ref node);
            Split(ref node.right);
        }

        return true;
    }

    private Node Search(Node node, TLimit start, TLimit end)
    {
        if (node == sentinel)
        {
            return null;
        }

        Interval interval = new Interval(start, end);
        int compare = interval.CompareTo(node.interval);
        if (compare < 0)
        {
            return Search(node.left, start, end);
        }
        else if (compare > 0)
        {
            return Search(node.right, start, end);
        }
        else
        {
            return node;
        }
    }

    public bool Add(TLimit start, TLimit end)
    {
		return Insert(ref root, start, end);
    }

    public bool Remove(TLimit start, TLimit end)
    {
        return Delete(ref root, start, end);
    }
	
	public IList<Interval> OverlappingIntervalsOf(
			TLimit start, TLimit end)
	{
		IList<Interval> intervals = new List<Interval>();
		Node node = Root;
		if(node != sentinel && node.value.CompareTo(end) > 0)
		{
			while(node != sentinel)
			{
				if(node.interval.End.CompareTo(end) > 0)
				{
					if(node.interval.Start.CompareTo(start) <= 0)
					{
						intervals.Add(node.interval);
						AddSubtreeToList(node.left, ref intervals);
                        node = node.right;
					}
					else 
					{
						node = node.left;
					}
				}
				else 
				{
					node = node.right;
				}
			}
		}

        return intervals;
	}
	
	private void AddSubtreeToList(Node subtree, ref IList<Interval> list) 
	{
		if(subtree != sentinel)
		{
			list.Add(subtree.interval);
			AddSubtreeToList(subtree.left, ref list);
			AddSubtreeToList(subtree.right, ref list);
		}
	}

    public void Traverse()
    {
        if (Root != sentinel)
        {
            Traverse(ref root);
        }
    }

    private void Traverse(ref Node node, string indent = "  ")
    {
        Console.WriteLine("{3}[Max:{0}, {1}-{2}]",
            node.value, node.interval.Start, node.interval.End, indent);
        if (node.right != sentinel)
        {
            Traverse(ref node.right, indent + "  R");
        }
        if (node.left != sentinel)
        {
            Traverse(ref node.left, indent + "  L");
        }
    }
}
