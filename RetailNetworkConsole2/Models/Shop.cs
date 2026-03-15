using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailNetworkConsole2.Models
{
    internal class Shop
    {
        public int ShopID { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Phone { get; set; }

        public ICollection<ShopSeller> ShopSellers { get; set; } = new List<ShopSeller>();
        public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
