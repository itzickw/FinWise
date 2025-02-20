using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AIModel; // חיבור למחלקות המודל

namespace AIManager.Services
{
    public class AIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _ollamaUrl = "http://localhost:11434/api/generate"; // כתובת ברירת מחדל של Ollama

        public AIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAIResponseAsync(string question)
        {
            var requestBody = new
            {
                model = "tinyllama:latest", // ודא שהמודל תואם למה שהתקנת
                prompt = question,
                stream = false // חשוב כדי למנוע stream ולתת תשובה רגילה
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(_ollamaUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Ollama API Error: {response.StatusCode}");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();

            // דסיריאליזציה עם תמיכה באותיות גדולות-קטנות
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<AIResponse>(jsonResponse, options);

            return result?.Response ?? "No response from AI";
        }
    }
}
