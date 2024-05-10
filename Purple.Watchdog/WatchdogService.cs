using Purple.Model;
using Purple.Model.Repositories;
using Purple.Watchdog.Monitors;
using Purple.Watchdog.Notifiers;

namespace Purple.Watchdog;

public class WatchdogService(AppDbContext dbContext, ISimilarDealsMonitor similarDealsMonitor, WatchdogConfiguration configuration, ISimilarDealRepository similarDealRepository, MockNotifier mockNotifier) : IWatchdogService
{
    public async Task ValidateOrder(Order order)
    {
        var similarDeals = await similarDealsMonitor.GetSimilarDealsAsync(order,
            TimeSpan.FromMilliseconds(configuration.TimeIntervalInMs), configuration.VolumeToBalanceRation);

        foreach (var similarTo in similarDeals)
        {
            var similarDeal = new SimilarDeal
            {
                Order = order,
                SimilarTo = similarTo
            };

            await mockNotifier.NotifyAsync(order, similarTo);
            similarDealRepository.Add(similarDeal);
        }

        await dbContext.SaveChangesAsync();
    }
}