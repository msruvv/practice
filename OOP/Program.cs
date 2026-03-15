using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Program
    {
        public class Product
        {
            private string _name;
            private string _manufacturer;
            private decimal _price;
            private DateTime _productionDate;
            private DateTime _expirationDate;

            public Product (string name, string manufacturer, decimal price, DateTime productionDate, DateTime expirationDate)
            {
                Name = name;
                Manufacturer = manufacturer;
                Price = price;
                ProductionDate = productionDate;
                ExpirationDate = expirationDate;
            }

            public string Name { get => _name; set => _name = value; }
            public string Manufacturer { get => _manufacturer; set => _manufacturer = value; }
            public decimal Price { get => _price; set => _price = value; }
            public DateTime ProductionDate { get => _productionDate; set => _productionDate = value; }
            public DateTime ExpirationDate { get => _expirationDate; set => _expirationDate = value; }

            public override string ToString()
            {
                return $"Товар: {Name}\n" +
                       $"Производитель: {Manufacturer}\n" +
                       $"Цена: {Price:F2} руб.\n" +
                       $"Дата производства: {ProductionDate:dd.MM.yyyy}\n" +
                       $"Срок годности: {ExpirationDate:dd.MM.yyyy}";
            }
        }
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
