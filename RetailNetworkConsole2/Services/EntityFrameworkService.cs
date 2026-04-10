using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailNetworkConsole2.Data;
using RetailNetworkConsole2.Models;
using static RetailNetworkConsole.Program;

namespace RetailNetworkConsole2.Services
{
    /// <summary>
    /// Сервис для работы с продавцами через Entity Framework
    /// </summary>
    internal class EntityFrameworkService : ISellerService
    {
        private readonly RetailContext _context;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EntityFrameworkService(RetailContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Создает нового продавца
        /// </summary>
        /// <param name="seller">Данные продавца</param>
        /// <returns>Созданный продавец</returns>
        public async Task<Seller> CreateSellerAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
            return seller;
        }

        /// <summary>
        /// Возвращает всех продавцов
        /// </summary>
        /// <returns>Список продавцов</returns>
        public async Task<List<Seller>> ReadAllSellersAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        /// <summary>
        /// Возвращает продавца по ID
        /// </summary>
        /// <param name="id">ID продавца</param>
        /// <returns>Продавец или null</returns>
        public async Task<Seller?> GetSellerByIdAsync(int id)
        {
            return await _context.Sellers
                .FirstOrDefaultAsync(s => s.SellerID == id);
        }

        /// <summary>
        /// Обновляет данные продавца
        /// </summary>
        /// <param name="seller">Новые данные продавца</param>
        /// <returns>True - успешно, False - не обновлено</returns>
        public async Task<bool> UpdateSellerAsync(Seller seller)
        {
            var existing = await _context.Sellers.FindAsync(seller.SellerID);
            if (existing == null) return false;

            existing.LastName = seller.LastName;
            existing.FirstName = seller.FirstName;
            existing.Patronymic = seller.Patronymic;
            existing.BirthDate = seller.BirthDate;
            existing.IsActive = seller.IsActive;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Удаляет продавца по ID
        /// </summary>
        /// <param name="id">ID продавца</param>
        /// <returns>True - успешно, False - не удалено</returns>
        public async Task<bool> DeleteSellerAsync(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null) return false;

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}