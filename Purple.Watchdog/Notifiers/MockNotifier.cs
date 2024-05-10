using Purple.Model;

namespace Purple.Watchdog.Notifiers;

public class MockNotifier
{
    public Task NotifyAsync(Order order, Order similarTo)
    {
        Console.WriteLine($"Order #{order.Id} is similar to #{similarTo.Id}");
        return Task.CompletedTask;
    }
}