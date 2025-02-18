using Market_data_manager;
using Market_data_model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MarketDataController : ControllerBase
{
    private readonly MarketDataManager _marketDataManager;

    public MarketDataController(MarketDataManager marketDataManager)
    {
        _marketDataManager = marketDataManager;
    }

    [HttpGet("latest/{symbol}")]
    public async Task<IActionResult> GetLatestStockData(string symbol)
    {
        try
        {
            DailyStockData? stockData = await _marketDataManager.GetLatestStockData(symbol);
            if (stockData == null)
                return NotFound(new { message = "Stock data not found" });

            return Ok(stockData);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Error fetching stock data: {ex.Message}" });
        }
    }
}
