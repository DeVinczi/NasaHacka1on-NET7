using Microsoft.EntityFrameworkCore;
using NasaHacka1on.Database.Models;

namespace NasaHacka1on.Database;

public abstract class CommunityCodeHubDataContext : CommunityCodeHubIdentityDataContext
{
    protected CommunityCodeHubDataContext()
    {
    }

    protected CommunityCodeHubDataContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<User> ApplicationUser { get; set; }
    public DbSet<Project> Projects{ get; set; }
    public DbSet<TechFocusModel> TechFocus { get; set; }
}
