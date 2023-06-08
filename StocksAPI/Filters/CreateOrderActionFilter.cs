using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts.DTO;
using StocksAPI.Controllers;
using StocksAPI.Models;

namespace StocksAPI.Filters
{
    public class CreateOrderActionFilter : IAsyncActionFilter
    {
        public CreateOrderActionFilter()
        {
            
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.Controller is TradeController tradeController)
            {
                var orderRequest = context.ActionArguments["sellOrderRequest"] as IOrderRequest;
                if (orderRequest != null)
                {
                    // update date to the current date
                    orderRequest.DateAndTimeOfOrder = DateTime.Now;
                    // perform model validations after updating the date
                    tradeController.ModelState.Clear();
                    tradeController.TryValidateModel(orderRequest);

                    if (!tradeController.ModelState.IsValid)
                    {
                        List<string> errors = tradeController.ModelState.Values
                            .SelectMany(e => e.Errors)
                            .Select(em => em.ErrorMessage).ToList();
                        StockTrade stockTrade = new StockTrade
                        {
                            StockName = orderRequest.StockName,
                            StockSymbol = orderRequest.StockSymbol,
                            Quantity = orderRequest.Quantity
                        };

                        context.Result = tradeController.Ok(stockTrade);
                    }
                    else
                    {
                        await next();
                    }
                }
                else
                {
                    await next();
                }
            }
            else
            {
                await next();
            }
        }
    }
}
