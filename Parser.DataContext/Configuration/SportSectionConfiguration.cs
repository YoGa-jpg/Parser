using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parser.DataContext.Entities;

namespace Parser.DataContext.Configuration
{
    public class SportSectionConfiguration : IEntityTypeConfiguration<SportSection>
    {
        public void Configure(EntityTypeBuilder<SportSection> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.HasMany(x => x.Students)
                .WithMany(x => x.SportSections);
        }
    }
}
