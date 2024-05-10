using Purple.Model;

namespace Purple.Watchdog.Monitors;

public interface ISimilarDealsMonitor
{
    Task<IEnumerable<Order>> GetSimilarDealsAsync(Order order, TimeSpan interval, decimal volumeToBalanceRatio);
}