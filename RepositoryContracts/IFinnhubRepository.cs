using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts;

public interface IFinnhubRepository
{
    /// <summary>
    /// Gets the company profile based on it's stock symbol
    /// </summary>
    /// <param name="stockSymbol">string parameter to set the stock symbol</param>
    /// <returns>a dictionary with a key: string and value: object</returns>
    Task<Dictionary<string, object>?> GetCompanyProfile(string? stockSymbol);
    /// <summary>
    /// Gets the current stock price
    /// </summary>
    /// <param name="stockSymbol">string parameter to set the stock symbol</param>
    /// <returns>a dictionary with a key: string and value: object</returns>
    Task<Dictionary<string, object>?> GetStockPriceQuote(string? stockSymbol);
    /// <summary>
    /// Gets a list of stocks available
    /// </summary>
    /// <returns>a list of dictionaries with key: string and value: string</returns>
    Task<List<Dictionary<string, string>>?> GetStocks();
    /// <summary>
    /// Searches for stocks
    /// </summary>
    /// <param name="stockSymbolToSearch">stock symbol used to search for stock</param>
    /// <returns>search results</returns>
    Task<Dictionary<string, object>?> SearchStocks(string? stockSymbolToSearch);
}
