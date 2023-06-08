using Entities;
using ServiceContracts.DTO;

namespace ServiceContracts.Extensions;

public static class DtoConversions
{
    // extension for sell order request type
    public static SellOrder ToSellOrder(this SellOrderRequest order)
    {
        return new SellOrder
        {
            StockName = order.StockName,
            StockSymbol = order.StockSymbol,
            Price = order.Price,
            DateAndTmeOfOrder = order.DateAndTimeOfOrder,
            Quantity = order.Quantity,
        };
    }

    // extension for sell order request type
    public static BuyOrder ToBuyOrder(this BuyOrderRequest order)
    {
        return new BuyOrder
        {
            StockName = order.StockName,
            StockSymbol = order.StockSymbol,
            Price = order.Price,
            DateAndTmeOfOrder = order.DateAndTimeOfOrder,
            Quantity = order.Quantity
        };
    }

    /// <summary>
    /// extension method to convert buyOrder entity to buyOrderResponse type
    /// </summary>
    /// <param name="buyOrder">buy order</param>
    /// <returns>BuyOrderResponse</returns>
    public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
    {
        return new BuyOrderResponse
        {
            BuyOrderID = buyOrder.BuyOrderID,
            StockName = buyOrder.StockName,
            StockSymbol = buyOrder.StockSymbol,
            DateAndTimeOfOrder = buyOrder.DateAndTmeOfOrder,
            Quantity = buyOrder.Quantity,
            Price = buyOrder.Price,
            TradeAmount = buyOrder.Quantity * buyOrder.Price
        };
    }

    /// <summary>
    /// extension method to convert sellOrder entity to sellOrderResponse type
    /// </summary>
    /// <param name="buyOrder">sell order</param>
    /// <returns>sellOrderResponse</returns>
    public static SellOrderResponse ToSellOrderResponse(this SellOrder sellOrder)
    {
        return new SellOrderResponse
        {
            SellOrderID = sellOrder.SellOrderID,
            StockName = sellOrder.StockName,
            StockSymbol = sellOrder.StockSymbol,
            DateAndTimeOfOrder = sellOrder.DateAndTmeOfOrder,
            Quantity = sellOrder.Quantity,
            Price = sellOrder.Price,
            TradeAmount = sellOrder.Quantity * sellOrder.Price
        };
    }
}
