namespace Parser.Domain.Models.Exceptions
{
    public class NoIdException : Exception
    {
        public NoIdException(string operation) : base($"Id is required for {operation} operation") { }
    }
}
