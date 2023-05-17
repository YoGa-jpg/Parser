using Parser.Core.Interfaces;

namespace Parser.Application.Services
{
    public class JsonParseService : IParseService
    {
        public Task<Dictionary<string, object>> Parse(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
