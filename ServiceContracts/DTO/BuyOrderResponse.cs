using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO;

public class BuyOrderResponse : IOrderResponse
{
    public Guid BuyOrderID { get; set; }
    public string StockSymbol { get; set; } = string.Empty;
    public string StockName { get; set; } = string.Empty;
    public DateTime DateAndTimeOfOrder { get; set; }
    public uint Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TradeAmount { get; set; }

    public OrderType TypeOfOrder => OrderType.BuyOrder;

    #region Overrides
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is not BuyOrderResponse) return false;

        BuyOrderResponse response = obj as BuyOrderResponse;
        return BuyOrderID == response.BuyOrderID
            && StockSymbol == response.StockSymbol
            && StockName == response.StockName
            && Price == response.Price
            && TradeAmount == response.TradeAmount;
    }

    public override string ToString()
    {
        return $"Buy Order ID: {BuyOrderID}, " +
            $"Stock Symbol: {StockSymbol}" +
            $"Stock Name: {StockName}" +
            $"Date and Time of Buy Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm ss tt")}, " +
            $"Quantity: {Quantity}" +
            $"Trade Amount: {TradeAmount}";
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
    #endregion
}
