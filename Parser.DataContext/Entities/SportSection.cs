using Parser.Domain.Models.Enums;

namespace Parser.DataContext.Entities
{
    public class SportSection
    {
        public Guid Id { get; set; }
        public Sections Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
