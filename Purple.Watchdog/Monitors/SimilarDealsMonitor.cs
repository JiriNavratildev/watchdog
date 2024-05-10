using Microsoft.EntityFrameworkCore;
using Purple.Model;
using Purple.Model.Repositories;

namespace Purple.Watchdog.Monitors;

public class SimilarDealsMonitor(IOrderRepository orderRepository) : ISimilarDealsMonitor
{
    public async Task<IEnumerable<Order>> GetSimilarDealsAsync(Order order, TimeSpan interval, decimal volumeToBalanceRatio)
    {
        var time = order.Time - interval;
        var similarOrders = await orderRepository.GetAll()
            .Where(x => x.Ticker == order.Ticker && x.Time >= time && x.Time >= order.Time && x.VolumeToBalanceRatio <= volumeToBalanceRatio)
            .ToListAsync();
        
        return similarOrders;
    }
}