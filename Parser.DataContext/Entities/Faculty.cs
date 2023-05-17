namespace Parser.DataContext.Entities
{
    public class Faculty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<Group> Groups { get; set; }
    }
}
