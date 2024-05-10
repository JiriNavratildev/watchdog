using Microsoft.EntityFrameworkCore;

namespace Purple.Model;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    
    public DbSet<Account> Accounts => Set<Account>();
}