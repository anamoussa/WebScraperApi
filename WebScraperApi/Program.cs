using Carter;
using Microsoft.EntityFrameworkCore;
using WebScraperApi.Handlers;
using WebScraperApi.Helpers;
using WebScraperApi.Models.Data;
using WebScraperApi.Services.Abstract;
using WebScraperApi.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScrapDBContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DBConnection"]));
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<IScraperService, ScraperService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCarter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.SeedingDB();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapCarter();
app.Run();
