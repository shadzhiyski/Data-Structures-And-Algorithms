using System;
using System.Collections.Generic;

public class Tree<T>
{
    private T value;
    public Tree<T> Parent;
    private IList<Tree<T>> children;

    public Tree(T value, params Tree<T>[] children)
    {
        Value = value;
        Children = new List<Tree<T>>();

        foreach (var child in children)
        {
            child.Parent = this;
            Children.Add(child);
        }
    }

    public T Value 
    {
        get { return this.value; }
        set { this.value = value; }
    }

    public IList<Tree<T>> Children
    {
        get { return children; }
        private set { children = value; } 
    }

    public void Print(int indent = 0)
    {
        Console.WriteLine("{0}{1}", new string(' ', 2 * indent), Value);

        foreach (var child in Children)
        {
            child.Print(indent + 1);
        }
    }

    public void Each(Action<T> action)
    {
        action(Value);

        foreach (var child in children)
        {
            child.Each(action);
        }
    }
}
