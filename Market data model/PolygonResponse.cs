using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Market_data_model
{
    public class PolygonResponse
    {
        [JsonPropertyName("results")]
        public List<PolygonStockResult> Results { get; set; } = new List<PolygonStockResult>();
    }
}
