using Parser.Domain.Models.Enums;

namespace Parser.Domain.Models
{
    public class SportSectionModel
    {
        public Guid Id { get; set; }
        public Sections Name { get; set; }

        public ICollection<StudentModel> Students { get; set; }
    }
}
