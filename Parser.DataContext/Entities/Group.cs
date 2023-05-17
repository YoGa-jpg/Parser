namespace Parser.DataContext.Entities
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public int Course { get; set; }

        public Guid FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
