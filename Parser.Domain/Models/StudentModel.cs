using System.Text.RegularExpressions;

namespace Parser.Domain.Models
{
    public class StudentModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public Guid GroupId { get; set; }
        public GroupModel Group { get; set; }
        public ICollection<AverageMarkModel> AverageMarks { get; set; }
        public ICollection<SportSectionModel> SportSections { get; set; }
    }
}
