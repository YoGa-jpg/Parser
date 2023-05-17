namespace Parser.DataContext.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<AverageMark> AverageMarks { get; set; }
        public ICollection<SportSection> SportSections { get; set; }
    }
}
