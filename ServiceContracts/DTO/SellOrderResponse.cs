namespace ServiceContracts.DTO;

public class SellOrderResponse : IOrderResponse
{
    public Guid SellOrderID { get; set; }
    public string StockSymbol { get; set; } = string.Empty;
    public string StockName { get; set; } = string.Empty;
    public DateTime DateAndTimeOfOrder { get; set; }
    public uint Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TradeAmount { get; set; }
    public OrderType TypeOfOrder => OrderType.SellOrder;

    #region Overrides
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is not SellOrderResponse) return false;

        SellOrderResponse response = obj as SellOrderResponse;
        return SellOrderID == response.SellOrderID
            && StockSymbol == response.StockSymbol
            && StockName == response.StockName
            && Price == response.Price
            && TradeAmount == response.TradeAmount;
    }

    public override string ToString()
    {
        return $"Buy Order ID: {SellOrderID}, " +
            $"Stock Symbol: {StockSymbol}" +
            $"Stock Name: {StockName}" +
            $"Date and Time of Buy Order: {DateAndTimeOfOrder:dd MMM yyyy hh:mm ss tt}, " +
            $"Quantity: {Quantity}" +
            $"Trade Amount: {TradeAmount}";
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
    #endregion
}
