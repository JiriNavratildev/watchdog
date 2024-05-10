namespace Purple.Model;

public class Order
{
    public string Id { get; set; }
    public string Server { get; set; }
    public decimal Balance { get; set; }
    public string Ticker { get; set; }
    public decimal Size { get; set; }
    public DateTime Time { get; set; }
    public decimal VolumeToBalanceRatio { get; set; }
}