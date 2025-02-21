using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Market_data_model
{
    public class PolygonStockResult
    {
        [JsonPropertyName("T")]
        public string Symbol { get; set; }

        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }

        [JsonPropertyName("c")]
        public decimal ClosePrice { get; set; }

        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }

        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }

        [JsonPropertyName("v")]
        public decimal Volume { get; set; }

        [JsonPropertyName("t")]
        public long Timestamp { get; set; }
    }
}
