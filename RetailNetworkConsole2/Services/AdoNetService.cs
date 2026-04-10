using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using RetailNetworkConsole2.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static RetailNetworkConsole.Program;

namespace RetailNetworkConsole2.Services
{
    /// <summary>
    /// Сервис для работы с продавцами через ADO.NET
    /// </summary>
    internal class AdoNetService : ISellerService
    {
        private readonly string _connectionString;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        /// <param name="configuration">Конфигурация приложения</param>
        public AdoNetService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found");
        }

        /// <summary>
        /// Создает нового продавца
        /// </summary>
        /// <param name="seller">Данные продавца</param>
        /// <returns>Созданный продавец</returns>
        public async Task<Seller> CreateSellerAsync(Seller seller)
        {
            const string sql = @"
            INSERT INTO Sellers (LastName, FirstName, Patronymic, BirthDate, IsActive)
            VALUES (@LastName, @FirstName, @Patronymic, @BirthDate, @IsActive);
            SELECT SCOPE_IDENTITY();";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {

                command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = seller.LastName;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = seller.FirstName;
                command.Parameters.Add("@Patronymic", SqlDbType.NVarChar, 100).Value = seller.Patronymic ?? (object)DBNull.Value;
                command.Parameters.Add("@BirthDate", SqlDbType.Date).Value = seller.BirthDate;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = seller.IsActive;
                await connection.OpenAsync();
                var newId = await command.ExecuteScalarAsync();

                return seller;
            }
        }

        /// <summary>
        /// Возвращает всех продавцов
        /// </summary>
        /// <returns>Список продавцов</returns>
        public async Task<List<Seller>> ReadAllSellersAsync()
        {
            const string sql = "SELECT SellerID, LastName, FirstName, Patronymic, BirthDate, IsActive FROM Sellers";

            var sellers = new List<Seller>();

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        sellers.Add(new Seller
                        {
                            SellerID = reader.GetInt32(0),
                            LastName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            Patronymic = reader.IsDBNull(3) ? null : reader.GetString(3),
                            BirthDate = reader.GetDateTime(4),
                            IsActive = reader.GetBoolean(5)
                        });
                    }
                }
            }

            return sellers;
        }

        /// <summary>
        /// Возвращает продавца по ID
        /// </summary>
        /// <param name="id">ID продавца</param>
        /// <returns>Продавец или null</returns>
        public async Task<Seller?> GetSellerByIdAsync(int id)
        {
            const string sql = @"
                SELECT SellerID, LastName, FirstName, Patronymic, BirthDate, IsActive
                FROM Sellers 
                WHERE SellerID = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Seller
                        {
                            SellerID = reader.GetInt32(0),
                            LastName = reader.GetString(1),
                            FirstName = reader.GetString(2),
                            Patronymic = reader.IsDBNull(3) ? null : reader.GetString(3),
                            BirthDate = reader.GetDateTime(4),
                            IsActive = reader.GetBoolean(5)
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Обновляет данные продавца
        /// </summary>
        /// <param name="seller">Новые данные продавца</param>
        /// <returns>True - успешно, False - не обновлено</returns>
        public async Task<bool> UpdateSellerAsync(Seller seller)
        {
            const string sql = @"
            UPDATE Sellers 
            SET LastName = @LastName, 
                FirstName = @FirstName, 
                Patronymic = @Patronymic, 
                BirthDate = @BirthDate, 
                IsActive = @IsActive
            WHERE SellerID = @SellerID";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@SellerID", SqlDbType.Int).Value = seller.SellerID;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = seller.LastName;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = seller.FirstName;
                command.Parameters.Add("@Patronymic", SqlDbType.NVarChar, 100).Value = seller.Patronymic ?? (object)DBNull.Value;
                command.Parameters.Add("@BirthDate", SqlDbType.Date).Value = seller.BirthDate;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = seller.IsActive;

                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        /// <summary>
        /// Удаляет продавца по ID
        /// </summary>
        /// <param name="id">ID продавца</param>
        /// <returns>True - успешно, False - не удалено</returns>
        public async Task<bool> DeleteSellerAsync(int id)
        {
            const string sql = "DELETE FROM Sellers WHERE SellerID = @SellerID";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@SellerID", SqlDbType.Int).Value = id;

                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}
