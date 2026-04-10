using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    /// <summary>
    /// Класс товара с информацией о продукте
    /// </summary>
    public class Product
    {
        private string _name;
        private string _manufacturer;
        private decimal _price;
        private DateTime _productionDate;
        private DateTime _expirationDate;

        /// <summary>
        /// Создает новый экземпляр товара
        /// </summary>
        /// <param name="name">Название товара</param>
        /// <param name="manufacturer">Производитель</param>
        /// <param name="price">Цена</param>
        /// <param name="productionDate">Дата производства</param>
        /// <param name="expirationDate">Срок годности</param>
        public Product(string name, string manufacturer,
            decimal price, DateTime productionDate, DateTime expirationDate)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
        }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Производитель товара
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        /// <exception cref="ArgumentException">Цена должна быть больше 0</exception>
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

        /// <summary>
        /// Дата производства
        /// </summary>
        /// <exception cref="ArgumentException">Дата должна быть позже 1900 года</exception>
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

        /// <summary>
        /// Срок годности
        /// </summary>
        /// <exception cref="ArgumentException">Дата должна быть позже 1900 года</exception>
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

        /// <summary>
        /// Возвращает строковое представление товара
        /// </summary>
        /// <returns>Информация о товаре</returns>
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
