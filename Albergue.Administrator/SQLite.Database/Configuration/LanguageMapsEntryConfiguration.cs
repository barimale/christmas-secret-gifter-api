using Albergue.Administrator.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Albergue.Administrator.Repository.Database.Configuration
{
    public class LanguageMapsEntryConfiguration : IEntityTypeConfiguration<LanguageMapEntry>
    {
        public void Configure(EntityTypeBuilder<LanguageMapEntry> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
