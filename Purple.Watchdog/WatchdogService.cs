using Purple.Model;
using Purple.Model.Repositories;
using Purple.Watchdog.Monitors;
using Purple.Watchdog.Notifiers;

namespace Purple.Watchdog;

public class WatchdogService(AppDbContext dbContext, ISimilarDealsMonitor similarDealsMonitor, WatchdogConfiguration configuration, ISimilarDealRepository similarDealRepository, MockNotifier mockNotifier) : IWatchdogService
{
    public async Task ValidateOrder(Deal deal)
    {
        var similarDeals = await similarDealsMonitor.GetSimilarDealsAsync(deal,
            TimeSpan.FromMilliseconds(configuration.TimeIntervalInMs), configuration.VolumeToBalanceRation);

        foreach (var similarTo in similarDeals)
        {
            var similarDeal = new SimilarDeal
            {
                Deal = deal,
                SimilarTo = similarTo
            };

            await mockNotifier.NotifyAsync(deal, similarTo);
            similarDealRepository.Add(similarDeal);
        }

        await dbContext.SaveChangesAsync();
    }
}