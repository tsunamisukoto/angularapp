using Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   
builder.Services.AddDbContext<EntityContext>(opt =>
{
    opt.UseInMemoryDatabase("Entities");
    opt.EnableDetailedErrors();
});
// Note: I'd locate this better in a proper project :P 
builder.Services.AddScoped<IValidator<CustomerInput.Create>, Models.CustomerInput.CreateValidator>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
