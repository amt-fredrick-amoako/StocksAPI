using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts;

public interface IStocksRepository
{
    /// <summary>
    /// Creates a buy order
    /// </summary>
    /// <param name="buyOrder">object of the buyorder</param>
    /// <returns>buy order that has been created</returns>
    Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);
    /// <summary>
    /// Creates a sell order
    /// </summary>
    /// <param name="sellOrder">object of the sellorder to be created</param>
    /// <returns>sell order that has been created</returns>
    Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

    /// <summary>
    /// Gets all existing buy orders in storage
    /// </summary>
    /// <returns>a list of buy orders in storage</returns>
    Task<List<BuyOrder>> GetBuyOrders();
    /// <summary>
    /// Gets all existing sell orders in storage
    /// </summary>
    /// <returns>a list of sell orders in storage</returns>
    Task<List<SellOrder>> GetSellOrders();
}
