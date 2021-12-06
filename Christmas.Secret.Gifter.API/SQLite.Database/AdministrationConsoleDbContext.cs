using Albergue.Administrator.Entities;
using Albergue.Administrator.Repository.Database.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Albergue.Administrator.Repository
{
    public sealed class AdministrationConsoleDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AdministrationConsoleDbContext(DbContextOptions<AdministrationConsoleDbContext> options)
        : base(options)
        {
            //Database.EnsureCreated();
            //intentionall left blank
        }

        public DbSet<ShopItemEntry> ShopItems { get; set; }
        public DbSet<ShopItemTranslatableDetailsEntry> ShopItemsTranslationDetails { get; set; }
        public DbSet<CategoryEntry> Categories { get; set; }
        public DbSet<CategoryTranslatableDetailsEntry> CategoriesTranslationDetails { get; set; }
        public DbSet<LanguageBaseEntry> Languages { get; set; }
        public DbSet<LanguageMapEntry> LanguageMaps { get; set; }
        public DbSet<ImageEntry> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ImageEntryConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageBaseEntryConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageMapsEntryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryTranslatableDetailsEntryConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityUserEntryConfiguration());
            modelBuilder.ApplyConfiguration(new ShopItemEntryConfiguration());
            modelBuilder.ApplyConfiguration(new ShopItemTranslatableDetailsEntryConfiguration());

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
