using Microsoft.EntityFrameworkCore;

namespace Purple.Model;

/// <remarks>
/// Add migrations using the following command inside the 'Purple.Watchdog' project directory:
///
/// dotnet ef migrations add --startup-project ../Purple.Watchdog --context AppDbContext [migration-name]
/// </remarks>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Deal> Orders => Set<Deal>();
    
    public DbSet<Account> Accounts => Set<Account>();
}