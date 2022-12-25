using DishShop.Lab2.Parsers.DomParser.Parser;
using DishShop.Lab2.Parsers.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*builder.Services.AddControllers(options=>{
    options.OutputFormatters.RemoveType(typeof(SystemTextJsonOutputFormatter));
    options.InputFormatters.RemoveType(typeof(SystemTextJsonInputFormatter));
    options.ReturnHttpNotAcceptable = true;
}).AddXmlSerializerFormatters();*/

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
