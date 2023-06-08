namespace ServiceContracts.FinnhubService;

public interface IFinnhubStockPriceQuoteService
{
    /// <summary>
    /// service method to get current stock price
    /// </summary>
    /// <param name="stockSymbol">stock symbol to get price</param>
    /// <returns>a dictionary results with a key:string and value:object pair</returns>
    Task<Dictionary<string, object>> GetStockPriceQuote(string? stockSymbol);
}
