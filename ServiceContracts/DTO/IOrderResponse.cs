namespace ServiceContracts.DTO;

public interface IOrderResponse
{
    string StockSymbol { get; set; }
    string StockName { get; set; }
    DateTime DateAndTimeOfOrder { get; set; }
    uint Quantity { get; set; }
    decimal Price { get; set; }
    OrderType TypeOfOrder { get; }
    decimal TradeAmount { get; set; }
}

public enum OrderType
{
    BuyOrder, SellOrder
}

