namespace Parser.Domain.Models
{
    public class GroupModel
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public int Course { get; set; }

        public Guid FacultyId { get; set; }
        public FacultyModel Faculty { get; set; }
        public ICollection<StudentModel> Students { get; set; }
    }
}
