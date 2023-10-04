using System.Collections;

namespace PasswordGenerator.CustomClasses
{
    public class CustomList<T> : IEnumerable<T>
    {
        private T[] _items = new T[4];
        private int _count = 0;

        public int Count => _count;

        public int Length
        {
            get => _items.Length;
            set => _items = new T[value];
        }
        public void Add(T item)
        {
            if (_count == Length)
            {
                ResizeArray();
            }

            _items[_count] = item;
            _count++;
        }

        public bool Remove(T item)
        {
            var index = Array.IndexOf(_items, item);

            if (index < 0) return false;
            Array.Copy(_items, index + 1, _items, index, Length - index - 1);
            _count--;
            return true;

        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ResizeArray()
        {
            T[] newArray = new T[Length * 2];
            Array.Copy(_items, newArray, _count);
            _items = newArray;
        }
        public void Clear()
        {
            Array.Clear(_items, 0, Length);
            _count = 0;
        }
        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }
    }
}