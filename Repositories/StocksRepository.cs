using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories;

public class StocksRepository : IStocksRepository
{
    private readonly ApplicationDbContext _db;

    public StocksRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
    {
        _db.BuyOrders.Add(buyOrder);
        await _db.SaveChangesAsync();
        return buyOrder;
    }

    public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
    {
        _db.SellOrders.Add(sellOrder);
        await _db.SaveChangesAsync();
        return sellOrder;
    }

    public async Task<List<BuyOrder>> GetBuyOrders()
    {
        List<BuyOrder>? buyOrders = await _db.BuyOrders
            .OrderByDescending(order => order.DateAndTmeOfOrder)
            .ToListAsync();
        return buyOrders;
    }

    public async Task<List<SellOrder>> GetSellOrders()
    {
        List<SellOrder>? sellOrders = await _db.SellOrders
            .OrderByDescending(order => order.DateAndTmeOfOrder)
            .ToListAsync();
        return sellOrders;
    }
}
