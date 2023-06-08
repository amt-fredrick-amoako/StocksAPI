using Entities;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.Extensions;
using ServiceContracts.StocksService;
using Services.Helpers;

namespace Services.StockService;

public class SellOrdersService : ISellOrdersService
{
    private readonly IStocksRepository _stocksRepository;

    public SellOrdersService(IStocksRepository stocksRepository)
    {
        _stocksRepository = stocksRepository;
    }

    public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? request)
    {
        // validation: sellOrderRequest should not be null
        if (request == null) throw new ArgumentNullException(nameof(request));
        //model validation using Validation helper
        ValidationHelpers.ModelValidation(request);

        // convert to sell order type
        SellOrder order = request.ToSellOrder();

        // generate a new guid for the order id
        order.SellOrderID = Guid.NewGuid();

        // add to sell orders list
        await _stocksRepository.CreateSellOrder(order);

        // convert order to sell order response type
        SellOrderResponse sellOrderResponse = order.ToSellOrderResponse();

        return sellOrderResponse;
    }

    public async Task<List<SellOrderResponse>> GetSellOrders()
    {
        List<SellOrder> sellOrders = await _stocksRepository.GetSellOrders();
        return sellOrders
            .Select(order => order.ToSellOrderResponse())
            .ToList();
    }
}
