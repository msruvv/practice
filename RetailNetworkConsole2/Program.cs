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

            Console.WriteLine("ДЕМОНСТРАЦИЯ РАБОТЫ С ADO.NET\n");
            await RunCrudDemo(adoNetService);

            Console.WriteLine("\n\nДЕМОНСТРАЦИЯ РАБОТЫ С ENTITY FRAMEWORK\n");
            await RunCrudDemo(efService);
        }

        static async Task RunCrudDemo(ISellerService service)
        {
            try
            {
                Console.WriteLine("1. ПОКАЗАТЬ ВСЕХ ПРОДАВЦОВ:");
                await ShowAllSellersAsync(service);
                Console.WriteLine();

                Console.WriteLine("2. ДОБАВИТЬ НЕСКОЛЬКО ПРОДАВЦОВ:");

                var sellersToAdd = new[]
                {
                    new Seller
                    {
                        LastName = "Иванов",
                        FirstName = "Иван",
                        Patronymic = "Иванович",
                        BirthDate = new DateTime(1990, 1, 1),
                        IsActive = true
                    },
                    new Seller
                    {
                        LastName = "Сидорова",
                        FirstName = "Анна",
                        Patronymic = "Петровна",
                        BirthDate = new DateTime(1995, 3, 15),
                        IsActive = true
                    },
                    new Seller
                    {
                        LastName = "Кузнецов",
                        FirstName = "Дмитрий",
                        Patronymic = null,
                        BirthDate = new DateTime(1988, 7, 20),
                        IsActive = false
                    }
                };

                var addedSellers = new List<Seller>();
                foreach (var seller in sellersToAdd)
                {
                    var added = await service.CreateSellerAsync(seller);
                    addedSellers.Add(added);
                    Console.WriteLine($"Добавлен: {added}");
                }
                Console.WriteLine();

                Console.WriteLine("3. ПОКАЗАТЬ ВСЕХ ПРОДАВЦОВ ПОСЛЕ ДОБАВЛЕНИЯ:");
                await ShowAllSellersAsync(service);
                Console.WriteLine();

                Console.WriteLine($"4. ОБНОВИТЬ ПРОДАВЦА (ID = {addedSellers[0].SellerID}):");
                addedSellers[0].LastName = "Иванов-Обновлен";
                addedSellers[0].IsActive = false;
                await service.UpdateSellerAsync(addedSellers[0]);
                Console.WriteLine($"Обновлен: {await service.GetSellerByIdAsync(addedSellers[0].SellerID)}");
                Console.WriteLine();

                Console.WriteLine($"5. УДАЛИТЬ ПРОДАВЦА (ID = {addedSellers[1].SellerID}):");
                await service.DeleteSellerAsync(addedSellers[1].SellerID);
                Console.WriteLine($"Продавец с ID {addedSellers[1].SellerID} удален");
                Console.WriteLine();

                Console.WriteLine("6. ПОКАЗАТЬ ВСЕХ ПРОДАВЦОВ ПОСЛЕ УДАЛЕНИЯ:");
                await ShowAllSellersAsync(service);
                Console.WriteLine();

                Console.WriteLine($"7. ПОПЫТКА ПОКАЗАТЬ УДАЛЕННОГО ПРОДАВЦА (ID = {addedSellers[1].SellerID}):");
                await GetSellerByIdAsync(service, addedSellers[1].SellerID);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Внутренняя ошибка: {ex.InnerException.Message}");
                }
            }
        }

        static async Task ShowAllSellersAsync(ISellerService service)
        {
            var sellers = await service.ReadAllSellersAsync();

            if (!sellers.Any())
            {
                Console.WriteLine("Продавцы не найдены");
                return;
            }

            foreach (var seller in sellers)
            {
                Console.WriteLine(seller);
            }
        }

        static async Task GetSellerByIdAsync(ISellerService service, int id)
        {
            var seller = await service.GetSellerByIdAsync(id);

            if (seller == null)
            {
                Console.WriteLine($"Продавец с ID {id} не найден");
            }
            else
            {
                Console.WriteLine(seller);
            }
        }
    }
}