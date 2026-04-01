using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    public class Product
    {
        private string _name;
        private string _manufacturer;
        private decimal _price;
        private DateTime _productionDate;
        private DateTime _expirationDate;

        public Product(string name, string manufacturer,
            decimal price, DateTime productionDate, DateTime expirationDate)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
        }

        public string Name { get; set; }
        public string Manufacturer { get; set; }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Цена должна быть больше 0");
                }
                _price = value;
            }
        }

        public DateTime ProductionDate
        {
            get => _productionDate;
            set
            {
                if (value.Year < 1900)
                {
                    throw new ArgumentException("Дата производства должна быть позже 1900 года");
                }
                _productionDate = value;
            }
        }

        public DateTime ExpirationDate
        {
            get => _expirationDate;
            set
            {
                if (value.Year < 1900)
                {
                    throw new ArgumentException("Срок годности должен быть позже 1900 года");
                }
                _expirationDate = value;
            }
        }

        public override string ToString()
        {
            return $"Товар: {Name}\n" +
                   $"Производитель: {Manufacturer}\n" +
                   $"Цена: {Price:F2} руб.\n" +
                   $"Дата производства: {ProductionDate:dd.MM.yyyy}\n" +
                   $"Срок годности: {ExpirationDate:dd.MM.yyyy}";
        }
    }
}
