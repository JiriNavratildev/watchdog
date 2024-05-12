using Microsoft.EntityFrameworkCore;

namespace Purple.Model;

public class AppDbContext : DbContext
{
    public DbSet<Deal> Orders => Set<Deal>();
    
    public DbSet<Account> Accounts => Set<Account>();
}