using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockModel
{
    public class Transaction
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string StockSymbol { get; set; }
        public decimal Quantity { get; set; }
        public string TransactionType { get; set; }
        public decimal Price { get; set; }  // המחיר בעת ביצוע העסקה
        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // זמן העסקה
    }
}
