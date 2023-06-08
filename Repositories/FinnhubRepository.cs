using Microsoft.Extensions.Configuration;
using RepositoryContracts;
using System.Text.Json;

namespace Repositories;

public class FinnhubRepository : IFinnhubRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public FinnhubRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    /// <summary>
    /// makes external api calls by acting as a client
    /// </summary>
    /// <returns>response message object</returns>
    private HttpResponseMessage ExternalApiCall()
    {
        // create http client
        HttpClient httpClient = _httpClientFactory.CreateClient();

        // make a request message object to send 
        HttpRequestMessage requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_configuration["FinnhubToken"]}")
        };
        // send request
        HttpResponseMessage responseMessage = httpClient.Send(requestMessage);

        // return response message
        return responseMessage;
    }

    /// <summary>
    /// Sends api calls to external api
    /// </summary>
    /// <param name="stockSymbol">stockSymbol to send api calls to</param>
    /// <returns>returns a HttpResponseMessage</returns>
    private HttpResponseMessage ExternalApiCall(string? stockSymbol)
    {
        // create http client
        HttpClient httpClient = _httpClientFactory.CreateClient();
        // make a request message object to send
        HttpRequestMessage requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
        };
        // send request
        HttpResponseMessage responseMessage = httpClient.Send(requestMessage);

        return responseMessage;
    }


    public async Task<Dictionary<string, object>?> GetCompanyProfile(string? stockSymbol)
    {
        //read response 
        string responseBody = new StreamReader(ExternalApiCall(stockSymbol).Content.ReadAsStream()).ReadToEnd();

        // convert response body from JSON to Dictionary
        Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody) ?? throw new InvalidOperationException("No response from server");
        if (responseDictionary.ContainsKey("error")) throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

        return responseDictionary;
    }

    public async Task<Dictionary<string, object>?> GetStockPriceQuote(string? stockSymbol)
    {
        // create http client
        HttpClient httpClient = _httpClientFactory.CreateClient();
        // make a request message object to send
        HttpRequestMessage requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
        };
        // send request
        HttpResponseMessage responseMessage = httpClient.Send(requestMessage);
        // read response
        string responseBody = await new StreamReader(responseMessage.Content.ReadAsStream()).ReadToEndAsync();

        // convert response body from JSON to Dictionary
        Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody) ?? throw new InvalidOperationException("No responses from server");
        if (responseDictionary.ContainsKey("error")) throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

        return responseDictionary;
    }

    public async Task<List<Dictionary<string, string>>?> GetStocks()
    {
        // read response
        string responseBody = await new StreamReader(ExternalApiCall().Content.ReadAsStream()).ReadToEndAsync();

        // convert response body from JSOn to Dictionary
        List<Dictionary<string, string>>? responseDictionaryList = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseBody) ?? throw new InvalidOperationException("No response from server");

        return responseDictionaryList;
    }

    public async Task<Dictionary<string, object>?> SearchStocks(string? stockSymbolToSearch)
    {
        // create http client
        HttpClient httpClient = _httpClientFactory.CreateClient();

        // make a request message object to send 
        HttpRequestMessage requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_configuration["FinnhubToken"]}")
        };
        // send request
        HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

        // read response to string
        string response = await new StreamReader(responseMessage.Content.ReadAsStream()).ReadToEndAsync();

        // DE serialize into a dictionary
        Dictionary<string, object> responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response) ?? throw new InvalidOperationException("No response from server");

        if (responseDictionary.ContainsKey("error")) throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

        return responseDictionary;
    }
}
