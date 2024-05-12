using Purple.Model;

namespace Purple.Watchdog;

public interface IWatchdogService
{
    Task ValidateOrderAsync(Deal deal);
}