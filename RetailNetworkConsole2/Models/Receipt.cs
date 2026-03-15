using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailNetworkConsole2.Models
{
    internal class Receipt
    {
        public Guid ReceiptID { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
        public int ShopID { get; set; }
        public int SellerID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime? SaleDate { get; set; }
        public string? PaymentType { get; set; }

        public Shop Shop { get; set; } = null!;
        public Seller Seller { get; set; } = null!;
    }
}
