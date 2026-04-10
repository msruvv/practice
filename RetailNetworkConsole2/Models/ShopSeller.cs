using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailNetworkConsole2.Models
{
    /// <summary>
    /// Связующая таблица между магазинами и продавцами (многие ко многим)
    /// </summary>
    internal class ShopSeller
    {
        public int ShopID { get; set; }
        public int SellerID { get; set; }

        public Shop Shop { get; set; } = null!;
        public Seller Seller { get; set; } = null!;
    }
}
