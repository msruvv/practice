using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    internal class Program
    {
        class SmartStack<T> : IEnumerable<T>
        {
            private T[] _items;
            private int _count;

            public SmartStack()
            {
                _items = new T[4];
                _count = 0;
            }

            public SmartStack(int capacity)
            {
                _items = new T[capacity];
                _count = 0;
            }

            public SmartStack(IEnumerable<T> collection)
            {
                int count = 0;
                foreach (T item in collection)
                    count++;

                _items = new T[count];
                _count = count;

                int index = count - 1;
                foreach (var item in collection)
                    _items[index--] = item;
            }

            public int Count => _count;

            public int Capacity => _items.Length;

            public void Push(T item)
            {
                if (_count == _items.Length)
                    Array.Resize(ref _items, _items.Length*2);

                _items[_count++] = item;
            }

            public void PushRange(IEnumerable<T> collection)
            {
                int itemsToAdd = 0;
                foreach(T item in collection) 
                    itemsToAdd++;
                int newCount = _count + itemsToAdd;

                if (newCount > _items.Length)
                {
                    int newCapacity = _items.Length;
                    while (newCapacity < newCount)
                    {
                        newCapacity *= 2;
                    }
                    Array.Resize(ref _items, newCapacity);
                }
                
                foreach (var item in collection)
                    _items[_count++] = item;
            }

            public T Pop()
            {
                if (_count == 0 )
                    throw new InvalidOperationException("Стек пуст");

                T item = _items[--_count];
                _items[_count] = default(T);
                return item;
            }

            public T Peek()
            {
                if (_count == 0 )
                    throw new InvalidOperationException("Стек пуст");

                return _items[_count - 1];
            }

            public bool Contains(T item)
            {
                for (var i = 0; i < _count; i++)
                {
                    if (Equals(_items[i], item))
                        return true;
                }
                return false;
            }

            public T this[int index]
            {
                get
                {
                    if (index < 0 || index >= _count)
                        throw new ArgumentOutOfRangeException(nameof(index));

                    return _items[_count - 1 - index];
                }

                set
                {
                    if (index < 0 || index >= _count)
                        throw new ArgumentOutOfRangeException(nameof(index));

                    _items[_count - 1 - index] = value;
                }
            }

            public IEnumerator<T> GetEnumerator()
            {
                for (var i = _count-1; i>=0; i--)
                    yield return _items[i];
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        static void Main(string[] args)
        {
            var stack = new SmartStack<int>();
            Console.WriteLine($"Начало: Count={stack.Count}, Capacity={stack.Capacity}");

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            Console.WriteLine($"После Push: Count={stack.Count}, Capacity={stack.Capacity}");

            Console.WriteLine("Все элементы:");
            for (int i = 0; i < stack.Count; i++)
            {
                Console.WriteLine(stack[i]);
            }

            Console.WriteLine($"Вершина: {stack.Peek()}");

            Console.WriteLine($"Есть 20? {stack.Contains(20)}");

            Console.WriteLine($"Глубина 1: {stack[1]}");

            Console.WriteLine($"Удален: {stack.Pop()}, осталось: {stack.Count}");

            Console.WriteLine("Все элементы:");
            foreach (var item in stack)
                Console.WriteLine(item);

            var list = new List<int> { 1, 2, 3 };
            var stack2 = new SmartStack<int>(list);
            Console.WriteLine($"Стек из коллекции: Count={stack2.Count}");

            stack2.PushRange(new[] { 4, 5 });
            Console.WriteLine($"После PushRange: Count={stack2.Count}");
        }
    }
}
