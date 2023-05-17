using Parser.Core.Interfaces;
using Parser.Domain.Models.Enums;

namespace Parser.Core.Delegates
{
    public delegate IParseService ParseDelegate(ParseTypes type);
}
