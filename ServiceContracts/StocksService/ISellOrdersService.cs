using ServiceContracts.DTO;

namespace ServiceContracts.StocksService;

public interface ISellOrdersService
{
    /// <summary>
    /// get sell order response with respect to it's associated sell order request object
    /// </summary>
    /// <param name="request">sell order request object</param>
    /// <returns>sell order request</returns>
    Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? request);
    /// <summary>
    /// get all sell orders in the data store
    /// </summary>
    /// <returns>returns a list of sell order response objects</returns>
    Task<List<SellOrderResponse>> GetSellOrders();
}
