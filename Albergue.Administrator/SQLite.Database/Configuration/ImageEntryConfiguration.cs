using Albergue.Administrator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text;

namespace Albergue.Administrator.Repository.Database.Configuration
{
    public class ImageEntryConfiguration : IEntityTypeConfiguration<ImageEntry>
    {
        public void Configure(EntityTypeBuilder<ImageEntry> builder)
        {
            var converter = new ValueConverter<string, byte[]>(
                v => Encoding.ASCII.GetBytes(v),
                v => Encoding.ASCII.GetString(v));

            builder.HasKey(o => o.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(p => p.ShopItem)
                .WithMany(pp => pp.Images)
                .HasForeignKey(ppp => ppp.ShopItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.ImageData).HasConversion(converter);
        }
    }
}
