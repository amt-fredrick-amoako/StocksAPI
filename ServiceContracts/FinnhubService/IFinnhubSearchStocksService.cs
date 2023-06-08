namespace ServiceContracts.FinnhubService;

public interface IFinnhubSearchStocksService
{
    /// <summary>
    /// service method to search stocks
    /// </summary>
    /// <param name="stockSymbolToSearch">stock symbol to search</param>
    /// <returns>returns dictionary results with key:string and value:object</returns>
    Task<Dictionary<string, object>> SearchStocksAsync(string? stockSymbolToSearch);
}
