using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailNetworkConsole2.Models
{
    /// <summary>
    /// Отзыв о магазине
    /// </summary>
    internal class Review
    {
        public Guid ReviewID { get; set; }
        public int ShopID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewDate { get; set; }

        public Shop Shop { get; set; } = null!;
    }
}
