using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите информацию о товаре:");

                Console.Write("Наименование: ");
                string name = Console.ReadLine();

                Console.Write("Производитель: ");
                string manufacturer = Console.ReadLine();

                Console.Write("Цена: ");
                decimal price = decimal.Parse(Console.ReadLine());

                Console.Write("Дата производства (дд.мм.гггг): ");
                DateTime productionDate = DateTime.ParseExact(Console.ReadLine(), "dd.mm.yyyy", null);

                Console.Write("Срок годности (дд.мм.гггг): ");
                DateTime expirationDate = DateTime.ParseExact(Console.ReadLine(), "dd.mm.yyyy", null);

                Product product = new Product(name, manufacturer, price, productionDate, expirationDate);

                Console.WriteLine("\nИнформация о товаре:");
                Console.WriteLine(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
