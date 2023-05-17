using Parser.Domain.Models.Enums;

namespace Parser.Domain.Models
{
    public class AverageMarkModel
    {
        public Guid Id { get; set; }
        public Subjects Subject { get; set; }
        public double SubjectMark { get; set; }

        public Guid StudentId { get; set; }
        public StudentModel Student { get; set; }
    }
}
