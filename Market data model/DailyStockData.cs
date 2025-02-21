using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_data_model
{
    public class DailyStockData
    {
        public DateTime Date { get; set; } // תאריך
        public decimal OpenPrice { get; set; } // מחיר פתיחה
        public decimal ClosePrice { get; set; } // מחיר סגירה
        public decimal HighPrice { get; set; } // מחיר מקסימלי
        public decimal LowPrice { get; set; } // מחיר מינימלי
        public long Volume { get; set; } // מחזור מסחר
    }

}
