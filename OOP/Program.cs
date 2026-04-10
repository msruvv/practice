using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Program
    {
        /// <summary>
        /// Главный метод программы
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите информацию о товаре:");

                string name = GetStringFromConsole("Наименование: ");
                string manufacturer = GetStringFromConsole("Производитель: ");
                decimal price = decimal.Parse(GetStringFromConsole("Цена: "));
                DateTime productionDate = DateTime.ParseExact(
                    GetStringFromConsole("Дата производства (дд.мм.гггг): "),
                    "dd.MM.yyyy",
                    null);
                DateTime expirationDate = DateTime.ParseExact(
                    GetStringFromConsole("Срок годности (дд.мм.гггг): "),
                    "dd.MM.yyyy",
                    null);

                Product product = new Product(name, manufacturer, price, productionDate, expirationDate);

                Console.WriteLine("\nИнформация о товаре:");
                Console.WriteLine(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Запрашивает у пользователя ввод строки
        /// </summary>
        /// <param name="prompt">Текст приглашения</param>
        /// <returns>Введенная строка</returns>
        static string GetStringFromConsole(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
