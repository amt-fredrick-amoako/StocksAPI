namespace ServiceContracts.FinnhubService;

public interface IFinnhubStocksService
{
    /// <summary>
    /// a service method to get list of stocks
    /// </summary>
    /// <returns>list of dictionaries with key:string and value:object pairs</returns>
    Task<List<Dictionary<string, string>>> GetStocksAsync();
}
