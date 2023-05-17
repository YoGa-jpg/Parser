namespace Parser.Core.Interfaces
{
    public interface IParseService
    {
        public Task<Dictionary<string, object>> Parse(Stream stream);
    }
}
