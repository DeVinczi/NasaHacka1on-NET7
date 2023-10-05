using Microsoft.EntityFrameworkCore;

namespace NasaHacka1on.Database;

internal class CommunityCodeHubSqlDataContext : CommunityCodeHubDataContext
{
    public CommunityCodeHubSqlDataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
