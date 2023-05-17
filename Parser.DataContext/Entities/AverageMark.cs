using Parser.Domain.Models.Enums;

namespace Parser.DataContext.Entities
{
    public class AverageMark
    {
        public Guid Id { get; set; }
        public Subjects Subject { get; set; }
        public double SubjectMark { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
