using ServiceContracts.DTO;

namespace StocksAPI.Models;

public class Order
{
    public List<BuyOrderResponse> BuyOrders { get; set; }
    public List<SellOrderResponse> SellOrders { get; set; }
}
