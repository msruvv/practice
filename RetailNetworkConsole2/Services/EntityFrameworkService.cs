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
    internal class EntityFrameworkService : ISellerService
    {
        private readonly RetailContext _context;

        public EntityFrameworkService(RetailContext context)
        {
            _context = context;
        }

        public async Task<Seller> CreateSellerAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
            return seller;
        }

        public async Task<List<Seller>> ReadAllSellersAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task<Seller?> GetSellerByIdAsync (int id)
        {
            return await _context.Sellers
                .FirstOrDefaultAsync(s => s.SellerID == id);
        }

        public async Task<bool> UpdateSellerAsync (Seller seller)
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
