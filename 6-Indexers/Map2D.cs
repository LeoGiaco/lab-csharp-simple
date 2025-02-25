namespace Indexers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;

    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        private IDictionary<Tuple<TKey1, TKey2>, TValue> ValueDictionary;

        public Map2D()
        {
            ValueDictionary = new Dictionary<Tuple<TKey1, TKey2>, TValue>();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements
        {
            get => ValueDictionary.Count;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get
            {
                if (key1 == null || key2 == null) throw new ArgumentNullException();
                var keyPair = new Tuple<TKey1, TKey2>(key1, key2);
                if (ValueDictionary.ContainsKey(keyPair))
                    return ValueDictionary[keyPair];
                else throw new KeyNotFoundException();
            }
            set
            {
                if (key1 == null || key2 == null) throw new ArgumentNullException();
                var keyPair = new Tuple<TKey1, TKey2>(key1, key2);
                if (!ValueDictionary.ContainsKey(keyPair))
                    ValueDictionary.Add(keyPair, value);
                else throw new ArgumentException("A value for both keys is already defined.");
            }
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            IList<Tuple<TKey2, TValue>> list = new List<Tuple<TKey2, TValue>>();
            foreach(var el in ValueDictionary)
                if(el.Key.Item1.Equals(key1))
                    list.Add(new Tuple<TKey2, TValue>(el.Key.Item2, el.Value));
            return list;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            IList<Tuple<TKey1, TValue>> list = new List<Tuple<TKey1, TValue>>();
            foreach (var el in ValueDictionary)
                if (el.Key.Item2.Equals(key2))
                    list.Add(new Tuple<TKey1, TValue>(el.Key.Item1, el.Value));
            return list;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            IList<Tuple<TKey1, TKey2, TValue>> list = new List<Tuple<TKey1, TKey2, TValue>>();
            foreach(KeyValuePair<Tuple<TKey1,TKey2>,TValue> v in ValueDictionary)
                list.Add(new Tuple<TKey1, TKey2, TValue>(v.Key.Item1, v.Key.Item2, v.Value));
            return list;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            foreach(TKey1 key1 in keys1)
                foreach (TKey2 key2 in keys2)
                    this[key1, key2] = generator(key1, key2);
        }

        /// <inheritdoc cref="IEquatable{T}.Equals(T)" />
        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            if (other == null)
                return false;
            if (base.Equals(other))
                return true;
            if (NumberOfElements != other.NumberOfElements)
                return false;
            var elems = GetElements();
            foreach (var el in other.GetElements())
                if (!elems.Contains(el))
                    return false;
            return true;
        }

        /// <inheritdoc cref="object.Equals(object?)" />
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (GetType() != obj.GetType())
                return false;
            return Equals(obj as Map2D<TKey1, TKey2, TValue>);
        }

        /// <inheritdoc cref="object.GetHashCode"/>
        public override int GetHashCode()
        {
            return GetElements().Aggregate(0, (x, y) => x.GetHashCode() ^ y.GetHashCode());
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.ToString"/>
        public override string ToString()
        {
            List<string> temp = new List<string>(NumberOfElements); 
            StringBuilder s = new StringBuilder("[");
            foreach(var el in GetElements())
            {
                temp.Add($"[Key1: {el.Item1}, Key2: {el.Item2}, Value: {el.Item3}");
            }
            s.Append(string.Join(", ", temp));
            s.Append("]");
            return s.ToString();
        }
    }
}
