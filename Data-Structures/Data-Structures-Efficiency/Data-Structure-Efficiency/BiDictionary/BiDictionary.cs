using System;
using System.Linq;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace BiDictionary
{
    public class BiDictionary<K1, K2, V>
    {
        private MultiDictionary<K1, KeyValueTuple<K1, K2, V>> byKey1;
        private MultiDictionary<K2, KeyValueTuple<K1, K2, V>> byKey2;
        private MultiDictionary<Tuple<K1, K2>, KeyValueTuple<K1, K2, V>> byKey1Key2;

        public BiDictionary(bool allowDublicateValues)
        {
            this.byKey1 = new MultiDictionary<K1, KeyValueTuple<K1, K2, V>>(allowDublicateValues);
            this.byKey2 = new MultiDictionary<K2, KeyValueTuple<K1, K2, V>>(allowDublicateValues);
            this.byKey1Key2 = new MultiDictionary<Tuple<K1, K2>, KeyValueTuple<K1, K2, V>>(allowDublicateValues);
        }

        public ICollection<KeyValueTuple<K1, K2, V>> Values
        {
            get { return this.byKey1Key2.Values.ToArray(); }
        }

        public int Count
        {
            get { return this.byKey1Key2.KeyValuePairs.Count; }
        }

        public void Add(K1 key1, K2 key2, V value)
        {
            var tuple = new KeyValueTuple<K1, K2, V>(key1, key2, value);
            var key1Key2 = new Tuple<K1, K2>(key1, key2);

            this.byKey1[key1].Add(tuple);
            this.byKey2[key2].Add(tuple);
            this.byKey1Key2[key1Key2].Add(tuple);
        }

        public ICollection<V> GetByFirstKey(K1 key1)
        {
            return this.byKey1[key1].Select(a => a.Value).ToArray();
        }

        public ICollection<V> GetBySecondKey(K2 key2)
        {
            return this.byKey2[key2].Select(a => a.Value).ToArray();
        }

        public ICollection<V> GetByTwoKeys(K1 key1, K2 key2)
        {
            return this.byKey1Key2[new Tuple<K1, K2>(key1, key2)].Select(a => a.Value).ToArray();
        }

        public void RemoveByFirstKey(K1 key1)
        {
            var values = this.byKey1[key1];

            foreach (var tuple in values)
            {
                this.byKey2.Remove(tuple.Key2, tuple);
                this.byKey1Key2.Remove(new Tuple<K1, K2>(tuple.Key1, tuple.Key2), tuple);
            }

            this.byKey1.Remove(key1);
        }

        public void RemoveBySecondKey(K2 key2)
        {
            var values = this.byKey2[key2];

            foreach (var tuple in values)
            {
                this.byKey1.Remove(tuple.Key1, tuple);
                this.byKey1Key2.Remove(new Tuple<K1, K2>(tuple.Key1, tuple.Key2), tuple);
            }

            this.byKey2.Remove(key2);
        }

        public void RemoveByTwoKeys(K1 key1, K2 key2)
        {
            var tuple = new Tuple<K1, K2>(key1, key2);
            var values = this.byKey1Key2[tuple];

            foreach (var value in values)
            {
                this.byKey1.Remove(key1, value);
                this.byKey2.Remove(key2, value);
            }

            this.byKey1Key2.Remove(tuple);
        }

        public class KeyValueTuple<K1, K2, V>
        {
            public KeyValueTuple(K1 key1, K2 key2, V value)
            {
                this.Key1 = key1;
                this.Key2 = key2;
                this.Value = value;
            }

            public K1 Key1 { get; private set; }

            public K2 Key2 { get; private set; }

            public V Value { get; private set; }

            public bool Equals(KeyValueTuple<K1, K2, V> other)
            {
                return !ReferenceEquals(other, null) &&
                       this.Key1.Equals(other.Key1) &&
                       this.Key2.Equals(other.Key2) &&
                       this.Value.Equals(other.Value);
            }

            public override bool Equals(object obj)
            {
                return this.Equals(obj as KeyValueTuple<K1, K2, V>);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int result = 17;

                    result = result * 23 + this.Key1.GetHashCode();
                    result = result * 23 + this.Key2.GetHashCode();
                    result = result * 23 + this.Value.GetHashCode();

                    return result;
                }
            }

            public override string ToString()
            {
                return string.Format("[{0}, {1}, {2}]", this.Key1, this.Key2, this.Value);
            }
        }
    }
}
