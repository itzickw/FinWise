using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Market_data_model; // ודא שאתה משתמש במודל המעודכן

namespace Market_data_manager
{
    public class AlphaVantageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY";
        private readonly string _apiKey = "R5PGW5TWXOVEIYAO";

        public AlphaVantageService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Dictionary<string, AlphaVantageStockResult>> GetStockDataAsync(string symbol, string outputsize)
        {
            string url = $"{_url}&symbol={symbol}&outputsize={outputsize}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to fetch stock data from AlphaVantage");

            var json = await response.Content.ReadAsStringAsync();

            // הדפסת התשובה כדי לבדוק שהמבנה תקין
            Console.WriteLine(json);

            var result = JsonSerializer.Deserialize<TimeSeriesResponse>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            if (result == null || result.TimeSeries == null)
                throw new Exception("Invalid response format from AlphaVantage");

            return result.TimeSeries;
        }

        public async Task<Dictionary<string, AlphaVantageStockResult>> GetStockDataForPeriodAsync(string symbol, int days)
        {
            string outputsize = days > 100 ? "full" : "compact";

            var timeSeries = await GetStockDataAsync(symbol, outputsize);

            var filteredData = timeSeries
                .Where(kv => DateTime.Parse(kv.Key) >= DateTime.UtcNow.AddDays(-days))
                .ToDictionary(kv => kv.Key, kv => kv.Value);

            return filteredData;
        }
    }
}
