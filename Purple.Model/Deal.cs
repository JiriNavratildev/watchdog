namespace Purple.Model;

public class Deal
{
    public string Id { get; set; }
    public string Server { get; set; }
    public string Direction { get; set; }  // BUY, SELL
    public string Ticker { get; set; }
    public decimal Size { get; set; }
    public DateTime Time { get; set; }
    public decimal VolumeToBalanceRatio { get; set; }
}