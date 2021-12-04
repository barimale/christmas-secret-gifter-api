using Albergue.Administrator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Albergue.Administrator.Repository.Database.Configuration
{
    public class CategoryEntryConfiguration : IEntityTypeConfiguration<CategoryEntry>
    {
        public void Configure(EntityTypeBuilder<CategoryEntry> builder)
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
