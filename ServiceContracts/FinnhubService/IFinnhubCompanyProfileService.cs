namespace ServiceContracts.FinnhubService;

public interface IFinnhubCompanyProfileService
{
    /// <summary>
    /// sevice method to get company profile
    /// </summary>
    /// <param name="stockSymbol">company profile symbol</param>
    /// <returns>dictionary with key:string and value:object</returns>
    Task<Dictionary<string, object>> GetCompanyProfileAsync(string? stockSymbol);
}
