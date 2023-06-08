using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts.DTO;
using ServiceContracts.FinnhubService;
using ServiceContracts.StocksService;
using StocksAPI.Filters;
using StocksAPI.Models;

namespace StocksAPI.Controllers
{
    public class TradeController : BaseController
    {
        private readonly TradingOptions _tradingOptions;
        private readonly IBuyOrdersService _buyOrdersService;
        private readonly ISellOrdersService _sellOrdersService;
        private readonly IFinnhubSearchStocksService _finnhubSearchStocksService;
        private readonly IFinnhubCompanyProfileService _finnhubCompanyProfileService;
        private readonly IFinnhubStockPriceQuoteService _finnhubStockPriceQuoteService;
        private readonly IConfiguration _configuration;

        public TradeController(IOptions<TradingOptions> tradingOptions, IBuyOrdersService buyOrdersService, ISellOrdersService sellOrdersService, IFinnhubSearchStocksService finnhubSearchStocksService, IFinnhubCompanyProfileService finnhubCompanyProfileService, IFinnhubStockPriceQuoteService finnhubStockPriceQuoteService, IConfiguration configuration)
        {
            _tradingOptions = tradingOptions.Value;
            _buyOrdersService = buyOrdersService;
            _sellOrdersService = sellOrdersService;
            _finnhubSearchStocksService = finnhubSearchStocksService;
            _finnhubCompanyProfileService = finnhubCompanyProfileService;
            _finnhubStockPriceQuoteService = finnhubStockPriceQuoteService;
            _configuration = configuration;
        }

        [HttpGet("[action]/{stockSymbol}")]
        public async Task<ActionResult> Index(string stockSymbol)
        {
            // reset stock symbol if not exists
            if (string.IsNullOrEmpty(stockSymbol)) stockSymbol = "MSFT";

            // get company profile from api server
            Dictionary<string, object>? companyProfileDictionary = await _finnhubCompanyProfileService.GetCompanyProfileAsync(stockSymbol);

            // get stock price quotes from api server
            Dictionary<string, object>? stockQuoteDictionary = await _finnhubStockPriceQuoteService.GetStockPriceQuote(stockSymbol);

            // create model object
            StockTrade stockTrade = new StockTrade() { StockSymbol = stockSymbol };

            // load data from finnhubservice into model object
            if (companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade
                {
                    StockSymbol = companyProfileDictionary["ticker"].ToString(),
                    StockName = companyProfileDictionary["name"].ToString(),
                    Quantity = _tradingOptions.DefaultOrderQuantity ?? 0,
                    Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString())
                };
            };

            return Ok(stockTrade);
        }

        [HttpPost("[action]")]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<ActionResult> BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now; // update date of order

            // re-validate the model object after updating the date
            ModelState.Clear();

            TryValidateModel(buyOrderRequest);

            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Values.SelectMany(e => e.Errors).Select(m => m.ErrorMessage).ToList();
                StockTrade stockTrade = new StockTrade
                {
                    StockName = buyOrderRequest.StockName,
                    Quantity = buyOrderRequest.Quantity,
                    StockSymbol = buyOrderRequest.StockSymbol
                };
                return BadRequest(stockTrade);
            }
            BuyOrderResponse buyOrderResponse = await _buyOrdersService.CreateBuyOrder(buyOrderRequest);
            return NoContent();
        }

        [HttpPost("[action]")]
        [TypeFilter(typeof(CreateOrderActionFilter))]
        public async Task<ActionResult> SellOrder(SellOrderRequest sellOrderRequest)
        {
            // update date of order
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            // re-validate the model object after updating the date
            ModelState.Clear();
            if (!ModelState.IsValid)
            {
                List<string> errors = ModelState.Values.SelectMany(e => e.Errors).Select(m => m.ErrorMessage).ToList();
                StockTrade stockTrade = new()
                {
                    StockName = sellOrderRequest.StockName,
                    Quantity = sellOrderRequest.Quantity,
                    StockSymbol = sellOrderRequest.StockSymbol
                };
                return BadRequest(stockTrade);
            }
            SellOrderResponse sellOrderResponse = await _sellOrdersService.CreateSellOrder(sellOrderRequest);
            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> Orders()
        {
            List<BuyOrderResponse> buyOrders = await _buyOrdersService.GetBuyOrders();
            List<SellOrderResponse> sellOrders = await _sellOrdersService.GetSellOrders();

            Order orders = new Order
            {
                BuyOrders = buyOrders,
                SellOrders = sellOrders
            };

            return Ok(orders);
        }
    }
}
