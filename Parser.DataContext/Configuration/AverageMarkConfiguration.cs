using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parser.DataContext.Entities;

namespace Parser.DataContext.Configuration
{
    public class AverageMarkConfiguration : IEntityTypeConfiguration<AverageMark>
    {
        public void Configure(EntityTypeBuilder<AverageMark> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Subject)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(x => x.SubjectMark)
                .IsRequired();

            builder.HasOne(x => x.Student)
                .WithMany(x => x.AverageMarks)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
