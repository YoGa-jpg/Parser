using Microsoft.AspNetCore.Mvc;
using Parser.Core.Interfaces;

namespace Parser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParserController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public ParserController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpPost]
        public async Task<IActionResult> Parse(IFormFile file)
        {
            await _operationService.ApplyFile(file);
            return Ok();
        }
    }
}
