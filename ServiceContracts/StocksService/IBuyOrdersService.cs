using ServiceContracts.DTO;

namespace ServiceContracts.StocksService;

public interface IBuyOrdersService
{
    /// <summary>
    /// Returns a buy order response object when a buy order request is made
    /// </summary>
    /// <param name="request">buy order request object</param>
    /// <returns>buy order response</returns>
    Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request);

    /// <summary>
    /// get all buy orders in a form of a buy order response object type
    /// </summary>
    /// <returns>returns a list of buy order response type</returns>
    Task<List<BuyOrderResponse>> GetBuyOrders();
}
