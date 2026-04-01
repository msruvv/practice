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
            {
                Console.WriteLine(item);
            }

            var list = new List<int> { 1, 2, 3 };
            var stack2 = new SmartStack<int>(list);
            Console.WriteLine($"Стек из коллекции: Count={stack2.Count}");

            stack2.PushRange(new[] { 4, 5 });
            Console.WriteLine($"После PushRange: Count={stack2.Count}");
        }
    }
}
