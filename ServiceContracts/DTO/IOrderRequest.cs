namespace ServiceContracts.DTO;

public interface IOrderRequest
{
    public string StockSymbol { get; set; }
    public string StockName { get; set; }
    public DateTime DateAndTimeOfOrder { get; set; }
    public uint Quantity { get; set; }
    public decimal Price { get; set; }
}
