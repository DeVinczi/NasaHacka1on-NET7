using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NasaHacka1on.Database.Models;

namespace NasaHacka1on.Database;

public abstract class CommunityCodeHubIdentityDataContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    protected CommunityCodeHubIdentityDataContext()
    {
    }

    protected CommunityCodeHubIdentityDataContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureUsers(builder);
        ConfigureProjectTypes(builder);
        SeedRoles(builder);
    }

    private static void ConfigureUsers(ModelBuilder modelBuilder)
    {
        var appUser = modelBuilder.Entity<ApplicationUser>();

        appUser.Property(x => x.DisplayName)
            .IsRequired()
            .HasMaxLength(20);
    }

    private static void ConfigureProjectTypes(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<User>();

        user.HasKey(u => u.Id);

        user.HasOne(u => u.ApplicationUser)
            .WithOne()
            .HasForeignKey<User>(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        user.Property(u => u.CreatedAt).IsRequired();

        user.HasMany(u => u.Projects)
            .WithOne(u => u.User)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        //var valueComparer = new ValueComparer<List<string>>(
        //    (c1, c2) => c1.SequenceEqual(c2),
        //    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        //    c => c.ToList());


        user.Property(u => u.Interests)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        //.Metadata
        //.SetValueComparer(valueComparer);
    }

    private static void SeedRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
            new IdentityRole<Guid>()
            {
                Name = "Admin",
                ConcurrencyStamp = "1",
                NormalizedName = "Admin",
                Id = Guid.Parse("3d055c3e-80c1-403a-9358-e31a3261cef3")
            },
            new IdentityRole<Guid>()
            {
                Name = "User",
                ConcurrencyStamp = "2",
                NormalizedName = "User",
                Id = Guid.Parse("9683e3db-7791-4858-a98b-c6f224f3e6e0")
            });
    }
}
