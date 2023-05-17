using Microsoft.AspNetCore.Http;

namespace Parser.Core.Interfaces
{
    public interface IOperationService
    {
        public Task ApplyFile(IFormFile file);
    }
}
