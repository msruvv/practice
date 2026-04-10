using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetailNetworkConsole2.Models;

namespace RetailNetworkConsole2
{
    /// <summary>
    /// Интерфейс сервиса для работы с продавцами
    /// </summary>
    internal interface ISellerService
    {
        Task<Seller> CreateSellerAsync(Seller seller);
        Task<List<Seller>> ReadAllSellersAsync();
        Task<Seller?> GetSellerByIdAsync(int id);
        Task<bool> UpdateSellerAsync(Seller seller);
        Task<bool> DeleteSellerAsync(int id);
    }
}
