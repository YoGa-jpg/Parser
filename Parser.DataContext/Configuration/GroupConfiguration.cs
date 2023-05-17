using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parser.DataContext.Entities;

namespace Parser.DataContext.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Number)
                .IsRequired();
            builder.Property(x => x.Course)
                .IsRequired();

            builder.HasOne(x => x.Faculty)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.FacultyId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
