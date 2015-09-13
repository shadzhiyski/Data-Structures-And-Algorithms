using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MyDictionary<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    public const float LoadFactor = 0.75f;
    public const int InitialCapacity = 16;
    private LinkedList<KeyValue<TKey, TValue>>[] slots;
    public int Count { get; private set; }

    public int Capacity
    {
        get
        {
            return slots.Length;
        }
    }

    public MyDictionary(int capacity = InitialCapacity)
    {
        slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        Count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        GrowIfNeeded();
        int slotNumber = FindSlotNumber(key);
        if (slots[slotNumber] == null)
        {
            slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var element in slots[slotNumber])
        {
            if (element.Key.Equals(key))
            {
                throw new ArgumentException("Key already exists: " + key);
            }
        }

        var newElement = new KeyValue<TKey, TValue>(key, value);
        slots[slotNumber].AddLast(newElement);
        Count++;
    }

    private void GrowIfNeeded()
    {
        if ((float)(Count + 1) / Capacity > LoadFactor)
        {
            Grow();
        }
    }

    private void Grow()
    {
        var newHashTable = new MyDictionary<TKey, TValue>(2 * Capacity);
        foreach (var element in this)
        {
            newHashTable.Add(element.Key, element.Value);
        }

        slots = newHashTable.slots;
        Count = newHashTable.Count;
    }

    private int FindSlotNumber(TKey key)
    {
        var slotNumber = Math.Abs(key.GetHashCode()) % slots.Length;
        return slotNumber;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        GrowIfNeeded();
        int slotNumber = FindSlotNumber(key);
        if (slots[slotNumber] == null)
        {
            slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var element in slots[slotNumber])
        {
            if (element.Key.Equals(key))
            {
                element.Value = value;
                return false;
            }
        }

        var newElement = new KeyValue<TKey, TValue>(key, value);
        slots[slotNumber].AddLast(newElement);
        Count++;
        return true;
    }

    public TValue Get(TKey key)
    {
        var element = Find(key);
        if (element == null)
        {
            throw new KeyNotFoundException();
        }

        return element.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            return Get(key);
        }
        set
        {
            AddOrReplace(key, value);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var element = Find(key);
        if (element != null)
        {
            value = element.Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int slotNumber = FindSlotNumber(key);
        var elements = slots[slotNumber];
        if (elements != null)
        {
            foreach (var element in elements)
            {
                if (element.Key.Equals(key))
                {
                    return element;
                }
            }
        }

        return null;
    }

    public bool ContainsKey(TKey key)
    {
        var element = Find(key);
        return element != null;
    }

    public bool Remove(TKey key)
    {
        int slotNumber = FindSlotNumber(key);
        var elements = slots[slotNumber];
        if (elements != null)
        {
            var currElement = elements.First;
            while (currElement != null)
            {
                if (currElement.Value.Key.Equals(key))
                {
                    elements.Remove(currElement);
                    Count--;
                    return true;
                }
                currElement = currElement.Next;
            }
        }

        return false;
    }

    public void Clear()
    {
        slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
        Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            return this.Select(e => e.Key);
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            return this.Select(e => e.Value);
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var elements in slots)
        {
            if (elements != null)
            {
                foreach (var element in elements)
                {
                    yield return element;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
