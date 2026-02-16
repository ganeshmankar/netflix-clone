using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NetflixClone.Infrastructure.Data;

/// <summary>
/// Design-time factory for creating DbContext during migrations
/// This allows EF Core tools to create the DbContext at design time
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<NetflixCloneDbContext>
{
    public NetflixCloneDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NetflixCloneDbContext>();
        
        // Use the same connection string as in appsettings.json
        optionsBuilder.UseSqlServer(
            "Server=Data Source=(localdb)\\mssqllocaldb;Initial Catalog=NetflixCloneDb;Integrated Security=True;Trust Server Certificate=True"
        );

        return new NetflixCloneDbContext(optionsBuilder.Options);
    }
}