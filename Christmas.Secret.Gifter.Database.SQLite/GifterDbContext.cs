using Christmas.Secret.Gifter.Database.SQLite.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Christmas.Secret.Gifter.Database.SQLite
{
    public sealed class GifterDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public GifterDbContext(DbContextOptions<GifterDbContext> options)
        : base(options)
        {
            //Database.EnsureCreated();
            //intentionall left blank
        }

        public DbSet<EventEntry> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new EventEntryConfiguration());
            //modelBuilder.ApplyConfiguration(new IdentityUserEntryConfiguration());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
