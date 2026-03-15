using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailNetworkConsole2.Models
{
    internal class ContactInfo
    {
        public int SellerID { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Address { get; set; }

        public Seller Seller { get; set; } = null!;
    }
}
