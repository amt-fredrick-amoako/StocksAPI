using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts.FinnhubService;
using StocksAPI.Models;

namespace StocksAPI.Controllers;

public class StocksController : BaseController
{
    private readonly TradingOptions _tradingOptions;
    private readonly IFinnhubStocksService _finnhubStocksService;

    public StocksController(IOptions<TradingOptions> options, IFinnhubStocksService finnhubStocksService)
    {
        _finnhubStocksService = finnhubStocksService;
        _tradingOptions = options.Value;
    }

    [HttpGet("[action]/{stock?}")]
    public async Task<ActionResult> GetStocks(bool showAll = false)
    {
        List<Dictionary<string, string>>? stocksDictionary = await _finnhubStocksService.GetStocksAsync(); // get company profile from external api
        List<Stock> stocks = new List<Stock>();

        if (stocksDictionary is not null)
        {
            // filter stocks
            if (!showAll && _tradingOptions.Top25PopularStocks != null)
            {
                string[]? Top25PopularStocksList = _tradingOptions.Top25PopularStocks.Split(',');
                if (Top25PopularStocksList is not null)
                {
                    stocksDictionary = stocksDictionary
                        .Where(stock => Top25PopularStocksList.Contains(Convert.ToString(stock["symbol"])))
                        .ToList();
                }
            }

            // convert dictionary objects into stock objects
            stocks = stocksDictionary
                .Select(stock => new Stock
                {
                    StockName = Convert.ToString(stock["description"]),
                    StockSymbol = Convert.ToString(stock["symbol"])
                }).Take(5).ToList();
        }

        return Ok(stocks);
    }
}
