using Exceptions;
using RepositoryContracts;
using ServiceContracts.FinnhubService;

namespace Services.FinnhubService;

public class FinnhubCompanyProfileService : IFinnhubCompanyProfileService
{
    private readonly IFinnhubRepository _finnHubRepository;

    public FinnhubCompanyProfileService(IFinnhubRepository finnHubRepository)
    {
        _finnHubRepository = finnHubRepository;
    }

    public async Task<Dictionary<string, object>> GetCompanyProfileAsync(string? stockSymbol)
    {
        try
        {
            Dictionary<string, object?> responseDictionary = await _finnHubRepository.GetCompanyProfile(stockSymbol);
            return responseDictionary;
        }
        catch (Exception ex)
        {
            FinnhubException finnhubException = new FinnhubException("Unable to connect to finnhub", ex);
            throw;
        }
    }
}
