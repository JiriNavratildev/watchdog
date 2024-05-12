using Purple.Model;

namespace Purple.Watchdog.Notifiers;

public class MockNotifier
{
    public Task NotifyAsync(Deal deal, Deal similarTo)
    {
        Console.WriteLine($"Order #{deal.Id} is similar to #{similarTo.Id}");
        return Task.CompletedTask;
    }
}