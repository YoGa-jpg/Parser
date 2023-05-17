using Microsoft.EntityFrameworkCore;
using Parser.Application.Services;
using Parser.Core.Delegates;
using Parser.Core.Interfaces;
using Parser.DataContext;
using Parser.DataContext.Repositories;
using Parser.DataContext.Repositories.Interfaces;
using Parser.Domain.Models.Enums;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ParserDataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddScoped<XmlParseService>();
builder.Services.AddScoped<IRepository, ParserRepository>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<JsonParseService>();

builder.Services.AddScoped<ParseDelegate>(serviceProvider => source =>
{
    return source switch
    {
        ParseTypes.Xml => serviceProvider.GetRequiredService<XmlParseService>(),
        ParseTypes.Json => serviceProvider.GetRequiredService<JsonParseService>(),
        _ => throw new InvalidOperationException()
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
