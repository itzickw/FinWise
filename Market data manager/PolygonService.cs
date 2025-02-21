using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Market_data_model;
using System.Text.Json.Serialization;

namespace Market_data_manager
{

    public class PolygonService
    {        
        private const string _apiKey = "EXBP72wblriTcUqVyHfRRKENzTfhxVhM"; // 🔹 הכנס את ה-API Key שלך
        private readonly HttpClient _httpClient;
        
        public PolygonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DailyStockData> GetLatestStockData(string symbol)
        {
            var client = new HttpClient();
            string url = $"https://api.polygon.io/v2/aggs/ticker/{symbol}/prev?adjusted=true&apiKey={_apiKey}";

            try
            {
                var response = await client.GetStringAsync(url);
                Console.WriteLine("Raw Polygon Response: " + JsonSerializer.Serialize(response));

                var polygonResponse = JsonSerializer.Deserialize<PolygonResponse>(response);
                if (polygonResponse == null || polygonResponse.Results == null)
                {
                    throw new Exception("Polygon response or results are null.");
                }

                Console.WriteLine(JsonSerializer.Serialize(polygonResponse));

                // המרת הנתונים לתוך מחלקת DailyStockData
                return new DailyStockData
                {
                    Date = DateTimeOffset.FromUnixTimeMilliseconds(polygonResponse?.Results[0]?.Timestamp ?? 0).DateTime,
                    OpenPrice = polygonResponse?.Results[0]?.OpenPrice ?? 0,
                    ClosePrice = polygonResponse?.Results[0]?.ClosePrice ?? 0,
                    HighPrice = polygonResponse?.Results[0]?.HighPrice ?? 0,
                    LowPrice = polygonResponse?.Results[0]?.LowPrice ?? 0,
                    Volume = (long)(polygonResponse?.Results[0]?.Volume ?? 0)
                };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching stock data: {ex.Message}");
                return null;
            }
        }
    }
}

