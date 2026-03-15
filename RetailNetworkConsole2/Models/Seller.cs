using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailNetworkConsole2.Models
{
    internal class Seller
    {
        public int SellerID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }

        public ContactInfo? ContactInfo { get; set; }
        public ICollection<ShopSeller> ShopSellers { get; set; } = new List<ShopSeller>();
        public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
    }
}
