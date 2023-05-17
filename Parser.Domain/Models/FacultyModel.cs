using System.Text.RegularExpressions;

namespace Parser.Domain.Models
{
    public class FacultyModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<GroupModel> Groups { get; set; }
    }
}
