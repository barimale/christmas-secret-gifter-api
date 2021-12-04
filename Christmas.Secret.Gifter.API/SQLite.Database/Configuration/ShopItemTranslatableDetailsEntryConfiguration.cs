using Albergue.Administrator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Albergue.Administrator.Repository.Database.Configuration
{
    public class ShopItemTranslatableDetailsEntryConfiguration : IEntityTypeConfiguration<ShopItemTranslatableDetailsEntry>
    {
        public void Configure(EntityTypeBuilder<ShopItemTranslatableDetailsEntry> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(p => p.ShopItem)
                .WithMany(pp => pp.TranslatableDetails)
                .HasForeignKey(ppp => ppp.ShopItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(p => p.Language)
               .WithOne(pp => pp.ShopItemTranslatableDetailsEntry)
               .HasForeignKey<LanguageMapEntry>(ppp => ppp.ShopItemTranslatableDetailsEntryId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
