using Albergue.Administrator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Albergue.Administrator.Repository.Database.Configuration
{
    public class ShopItemEntryConfiguration : IEntityTypeConfiguration<ShopItemEntry>
    {
        public void Configure(EntityTypeBuilder<ShopItemEntry> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(p => p.Category)
                .WithMany(pp => pp.ShopItems)
                .HasForeignKey(ppp => ppp.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Active).HasConversion<bool>();
        }
    }
}
