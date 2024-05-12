namespace Purple.Model.Repositories;

public interface IOrderRepository
{
    IQueryable<Deal> GetAll();
}