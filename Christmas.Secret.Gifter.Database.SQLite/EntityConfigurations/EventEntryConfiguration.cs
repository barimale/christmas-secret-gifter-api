using Christmas.Secret.Gifter.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Christmas.Secret.Gifter.Infrastructure.EntityConfigurations
{
    public class EventEntryConfiguration : IEntityTypeConfiguration<EventEntry>
    {
        public void Configure(EntityTypeBuilder<EventEntry> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
