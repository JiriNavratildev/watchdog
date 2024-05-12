namespace Purple.Model;

public class SimilarDeal
{
    public Guid DealId { get; set; }
    
    public Guid SimilarToId { get; set; }
    
    public Deal Deal { get; set; }
    
    public Deal SimilarTo { get; set; }
}