using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    /// <summary>
    /// Умный стек
    /// </summary>
    /// <typeparam name="T">Тип элементов стека</typeparam>
    class SmartStack<T> : IEnumerable<T>
    {
        private T[] _items;
        private int _count;

        /// <summary>
        /// Создает пустой стек с емкостью 4
        /// </summary>
        public SmartStack()
        {
            _items = new T[4];
            _count = 0;
        }

        /// <summary>
        /// Создает пустой стек с указанной емкостью
        /// </summary>
        /// <param name="capacity">Начальная емкость стека</param>
        public SmartStack(int capacity)
        {
            _items = new T[capacity];
            _count = 0;
        }

        /// <summary>
        /// Создает стек и заполняет его элементами из коллекции
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        public SmartStack(IEnumerable<T> collection)
        {
            int count = 0;
            foreach (T item in collection)
            {
                count++;
            }

            _items = new T[count];
            _count = count;

            int index = count - 1;
            foreach (var item in collection)
            {
                _items[index--] = item;
            }
        }

        /// <summary>
        /// Количество элементов в стеке
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Текущая емкость стека
        /// </summary>
        public int Capacity => _items.Length;

        /// <summary>
        /// Добавляет элемент в стек
        /// </summary>
        /// <param name="item">Добавляемый элемент</param>
        public void Push(T item)
        {
            if (_count == _items.Length)
            {
                Array.Resize(ref _items, _items.Length * 2);
            }

            _items[_count++] = item;
        }

        /// <summary>
        /// Добавляет несколько элементов в стек
        /// </summary>
        /// <param name="collection">Коллекция элементов</param>
        public void PushRange(IEnumerable<T> collection)
        {
            int itemsToAdd = 0;
            foreach (T item in collection)
            {
                itemsToAdd++;
            }
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
            {
                _items[_count++] = item;
            }
        }

        /// <summary>
        /// Извлекает верхний элемент из стека
        /// </summary>
        /// <returns>Верхний элемент</returns>
        /// <exception cref="InvalidOperationException">Стек пуст</exception>
        public T Pop()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Стек пуст");
            }

            T item = _items[--_count];
            _items[_count] = default(T);
            return item;
        }

        /// <summary>
        /// Возвращает верхний элемент без его удаления
        /// </summary>
        /// <returns>Верхний элемент</returns>
        /// <exception cref="InvalidOperationException">Стек пуст</exception>
        public T Peek()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("Стек пуст");
            }

            return _items[_count - 1];
        }

        /// <summary>
        /// Проверяет, содержится ли элемент в стеке
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>True - элемент найден, False - не найден</returns>
        public bool Contains(T item)
        {
            for (var i = 0; i < _count; i++)
            {
                if (Equals(_items[i], item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Индексатор для доступа к элементам стека
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>Элемент по индексу</returns>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return _items[_count - 1 - index];
            }

            set
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                _items[_count - 1 - index] = value;
            }
        }

        /// <summary>
        /// Возвращает перечислитель элементов стека
        /// </summary>
        /// <returns>Перечислитель</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = _count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
