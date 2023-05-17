namespace Parser.Domain.Models.Exceptions
{
    public class WrongFieldTypeException : Exception
    {
        public WrongFieldTypeException() : base("Can't parse field into this type") { }
    }
}
