using Purple.Model;

namespace Purple.Watchdog.Monitors;

public interface ISimilarDealsMonitor
{
    Task<IEnumerable<Deal>> GetSimilarDealsAsync(Deal deal, TimeSpan interval, decimal volumeToBalanceRatio);
}