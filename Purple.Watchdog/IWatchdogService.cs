using Purple.Model;

namespace Purple.Watchdog;

public interface IWatchdogService
{
    Task ValidateOrder(Deal deal);
}