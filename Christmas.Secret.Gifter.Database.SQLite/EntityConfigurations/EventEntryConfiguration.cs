using Christmas.Secret.Gifter.Database.SQLite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Christmas.Secret.Gifter.Database.SQLite.SQLite.Database.Configuration
{
    public class EventEntryConfiguration : IEntityTypeConfiguration<EventEntry>
    {
        public void Configure(EntityTypeBuilder<EventEntry> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            //builder.HasMany(dm => dm.TranslatableDetails)
            //    .WithOne(p => p.Category)
            //    .OnDelete(DeleteBehavior.Cascade);

            //builder.HasData(
            //        new CategoryEntry { Id = Guid.NewGuid().ToString(), Name = "ALL" }
            //    );
        }
    }
}
