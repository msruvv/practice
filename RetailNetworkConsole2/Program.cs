using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetailNetworkConsole2.Data;
using RetailNetworkConsole2.Models;
using RetailNetworkConsole2.Services;
using RetailNetworkConsole2;

namespace RetailNetworkConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("РАБОТА С БАЗОЙ ДАННЫХ\n");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<AdoNetService>();
            services.AddScoped<EntityFrameworkService>();

            services.AddDbContext<RetailContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var serviceProvider = services.BuildServiceProvider();

            var adoNetService = serviceProvider.GetRequiredService<AdoNetService>();
            var efService = serviceProvider.GetRequiredService<EntityFrameworkService>();

            while (true)
            {
                Console.WriteLine(
                    $"\nВыберите действие:\n" +
                    $"1. ADO.NET - показать всех продавцов\n" +
                    $"2. ADO.NET - добавить нового продавца\n" +
                    $"3. ADO.NET - обновить данные продавца\n" +
                    $"4. ADO.NET - удалить продавца\n" +
                    $"5. ADO.NET - показать продавца по ID\n" +
                    $"6. Entity Framework - показать всех продавцов\n" +
                    $"7. Entity Framework - добавить продавца\n" +
                    $"8. Entity Framework - обновить продавца\n" +
                    $"9. Entity Framework - удалить продавца\n" +
                    $"10. Entity Framework - показать продавца по ID\n" +
                    $"0. Выход");

                Console.Write("\nВаш выбор: ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await ShowAllSellersAsync(adoNetService);
                            break;
                        case "2":
                            await AddNewSellerAsync(adoNetService);
                            break;
                        case "3":
                            await UpdateSellerAsync(adoNetService);
                            break;
                        case "4":
                            await DeleteSellerAsync(adoNetService);
                            break;
                        case "5":
                            await GetSellerByIdAsync(adoNetService);
                            break;
                        case "6":
                            await ShowAllSellersAsync(efService);
                            break;
                        case "7":
                            await AddNewSellerAsync(efService);
                            break;
                        case "8":
                            await UpdateSellerAsync(efService);
                            break;
                        case "9":
                            await DeleteSellerAsync(efService);
                            break;
                        case "10":
                            await GetSellerByIdAsync(efService);
                            break;
                        case "0":
                            Console.WriteLine("Программа завершена.");
                            return;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
                    }
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static async Task ShowAllSellersAsync(ISellerService service)
        {
            Console.WriteLine("\n--- ВСЕ ПРОДАВЦЫ ---");
            var sellers = await service.ReadAllSellersAsync();

            foreach (var seller in sellers)
                seller.ToString();
        }

        static async Task AddNewSellerAsync(ISellerService service)
        {
            Console.WriteLine($"\n--- ДОБАВЛЕНИЕ НОВОГО ПРОДАВЦА ---");

            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine() ?? "Иванов";

            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine() ?? "Иван";

            Console.Write("Введите отчество (можно оставить пустым): ");
            string patronymic = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(patronymic)) patronymic = null;

            Console.Write("Введите дату рождения (ГГГГ-ММ-ДД): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime birthDate))
                birthDate = new DateTime(1990, 1, 1);

            Console.Write("Активен? (да/нет): ");
            string isActiveStr = Console.ReadLine()?.ToLower();
            bool isActive = isActiveStr == "да" || isActiveStr == "yes" || isActiveStr == "1" || isActiveStr == "true";

            var seller = new Seller
            {
                LastName = lastName,
                FirstName = firstName,
                Patronymic = patronymic,
                BirthDate = birthDate,
                IsActive = isActive
            };

            Seller newSeller = await service.CreateSellerAsync(seller);
            Console.WriteLine($"Продавец добавлен\n");
            newSeller.ToString();
        }

        static async Task UpdateSellerAsync(ISellerService service)
        {
            Console.WriteLine("\n--- ОБНОВЛЕНИЕ ДАННЫХ ПРОДАВЦА ---");

            Console.Write("Введите ID продавца для обновления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID");
                return;
            }

            var existingSeller = await service.GetSellerByIdAsync(id);
            if (existingSeller == null)
            {
                Console.WriteLine($"Продавец с ID {id} не найден");
                return;
            }

            Console.WriteLine($"\nТекущие данные продавца:");
            existingSeller.ToString();

            Console.WriteLine("\nВведите новые данные:");

            Console.Write($"Фамилия ({existingSeller.LastName}): ");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
                existingSeller.LastName = lastName;

            Console.Write($"Имя ({existingSeller.FirstName}): ");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(firstName))
                existingSeller.FirstName = firstName;

            Console.Write($"Отчество ({existingSeller.Patronymic ?? "не указано"}): ");
            string patronymic = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(patronymic))
            {
                existingSeller.Patronymic = patronymic;
            }
            else if (patronymic == "")
            {
                existingSeller.Patronymic = null;
            }

            Console.Write($"Дата рождения ({existingSeller.BirthDate:dd.MM.yyyy}): ");
            string birthDateStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(birthDateStr)
                && DateTime.TryParse(birthDateStr, out DateTime newBirthDate))
            {
                existingSeller.BirthDate = newBirthDate;
            }

            Console.Write($"Активен (1/0) ({(existingSeller.IsActive ? 1 : 0)}): ");
            string isActiveStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(isActiveStr))
            {
                existingSeller.IsActive = true;
            }

            bool result = await service.UpdateSellerAsync(existingSeller);

            if (result)
            {
                Console.WriteLine($"\nПродавец с ID {id} успешно обновлен!");
                var updated = await service.GetSellerByIdAsync(id);
                Console.WriteLine($"\nОбновленные данные:");
                updated.ToString();
            }
            else
            {
                Console.WriteLine($"\nНе удалось обновить продавца с ID {id}");
            }
        }
        static async Task DeleteSellerAsync(ISellerService service)
        {
            Console.WriteLine("\n--- УДАЛЕНИЕ ПРОДАВЦА ---");

            Console.Write("Введите ID продавца для удаления: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID");
                return;
            }

            var seller = await service.GetSellerByIdAsync(id);
            if (seller == null)
            {
                Console.WriteLine($"Продавец с ID {id} не найден");
                return;
            }

            Console.WriteLine($"Найден продавец: {seller.LastName} {seller.FirstName}");
            Console.Write("Вы уверены, что хотите удалить этого продавца? (да/нет): ");
            string confirm = Console.ReadLine()?.ToLower();

            if (confirm == "да")
            {
                bool result = await service.DeleteSellerAsync(id);
                Console.WriteLine(result
                    ? $"Продавец с ID {id} успешно удален"
                    : $"Не удалось удалить продавца с ID {id}");
            }
            else
            {
                Console.WriteLine("Удаление отменено");
            }
        }

        static async Task GetSellerByIdAsync(ISellerService service)
        {
            Console.WriteLine("\n--- ПОИСК ПРОДАВЦА ПО ID ---");

            Console.Write("Введите ID продавца: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID");
                return;
            }

            var seller = await service.GetSellerByIdAsync(id);

            if (seller == null)
            {
                Console.WriteLine($"Продавец с ID {id} не найден");
            }
            else
            {
                seller.ToString();
            }
        }
    }
}