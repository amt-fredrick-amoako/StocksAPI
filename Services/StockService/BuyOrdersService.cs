using Entities;
using RepositoryContracts;
using ServiceContracts.DTO;
using ServiceContracts.Extensions;
using ServiceContracts.StocksService;
using Services.Helpers;

namespace Services.StockService
{
    public class BuyOrdersService : IBuyOrdersService
    {
        private readonly IStocksRepository _stocksRepository;

        public BuyOrdersService(IStocksRepository stocksRepository)
        {
            _stocksRepository = stocksRepository;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? request)
        {
            try
            {
                if(request == null) throw new ArgumentNullException(nameof(request)); // buyOrderRequest should not be null
                // model validation
                ValidationHelpers.ModelValidation(request);

                // convert buy order request into buy order type
                BuyOrder buyOrder = request.ToBuyOrder();
                // generate new guid
                buyOrder.BuyOrderID = Guid.NewGuid();

                // add buy order to the orders list
                await _stocksRepository.CreateBuyOrder(buyOrder);

                // convert and return buy order as response type
                return buyOrder.ToBuyOrderResponse();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            try
            {
                List<BuyOrder> buyOrders = await _stocksRepository.GetBuyOrders();
                return buyOrders
                    .Select(order => order.ToBuyOrderResponse())
                    .ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
