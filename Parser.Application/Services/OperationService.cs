using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Parser.Core.Delegates;
using Parser.Core.Interfaces;
using Parser.DataContext.Repositories;
using Parser.DataContext.Repositories.Interfaces;
using Parser.Domain.Models.Enums;
using System.Reflection;

namespace Parser.Application.Services
{
    public class OperationService : IOperationService
    {
        private readonly ParseDelegate _parseDelegate;
        private readonly IRepository _repository;
        private readonly ILogger<OperationService> _logger;
        private IParseService _parseService;

        public OperationService(IRepository repository, ParseDelegate parseDelegate, ILogger<OperationService> logger)
        {
            _repository = repository;
            _parseDelegate = parseDelegate;
            _logger = logger;
        }

        public async Task ApplyFile(IFormFile file)
        {
            switch (file.ContentType)
            {
                case "text/xml":
                    _parseService = _parseDelegate(ParseTypes.Xml);
                    break;
                case "text/json":
                    _parseService = _parseDelegate(ParseTypes.Json);
                    break;
            }
            var data = await _parseService.Parse(file.OpenReadStream());

            foreach (var item in data)
            {
                switch (item.Key.ToLower())
                {
                    case "create":
                        await Create(item.Value);
                        break;
                    case "update":
                        await Update(item.Value);
                        break;
                    case "delete":
                        await Delete(item.Value);
                        break;
                    default:
                        await Update(item.Value);
                        break;
                }
            }

            _repository.Save();
        }

        private async Task Create(object item)
        {
            MethodInfo method = typeof(ParserRepository).GetMethod(nameof(ParserRepository.Insert));
            MethodInfo generic = method.MakeGenericMethod(item.GetType());

            generic.Invoke(_repository, new object[] { item });
        }

        private async Task Update(object item)
        {
            MethodInfo method = typeof(ParserRepository).GetMethod(nameof(ParserRepository.Update));
            MethodInfo generic = method.MakeGenericMethod(item.GetType());

            generic.Invoke(_repository, new object[] { item });
        }

        private async Task Delete(object item)
        {
            {
                var type = item.GetType();

                MethodInfo method = typeof(ParserRepository).GetMethod(nameof(ParserRepository.Delete));
                MethodInfo generic = method.MakeGenericMethod(type);

                generic.Invoke(_repository, new object[] { type.GetProperty("Id").GetValue(item) });
            }
        }
    }
}
