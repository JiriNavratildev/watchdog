namespace Purple.Model;

public class SimilarDeal
{
    public Guid OrderId { get; set; }
    
    public Guid SimilarToId { get; set; }
    
    public Order Order { get; set; }
    
    public Order SimilarTo { get; set; }
}