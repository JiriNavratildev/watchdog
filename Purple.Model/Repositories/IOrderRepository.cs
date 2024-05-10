namespace Purple.Model.Repositories;

public interface IOrderRepository
{
    IQueryable<Order> GetAll();
}