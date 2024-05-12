using Microsoft.EntityFrameworkCore;
using Purple.Model;
using Purple.Model.Repositories;

namespace Purple.Watchdog.Monitors;

public class SimilarDealsMonitor(IOrderRepository orderRepository) : ISimilarDealsMonitor
{
    public async Task<IEnumerable<Deal>> GetSimilarDealsAsync(Deal deal, TimeSpan interval, decimal volumeToBalanceRatio)
    {
        var beginInterval = deal.Time - interval;
        var similarOrders = await orderRepository.GetAll()
            .Where(x => x.Ticker == deal.Ticker 
                        && x.Time >= beginInterval 
                        && x.Time <= deal.Time
                        && x.VolumeToBalanceRatio <= volumeToBalanceRatio)
            .ToListAsync();
        
        return similarOrders;
    }
}