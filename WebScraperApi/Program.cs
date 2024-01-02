
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ScrapDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"))
);
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
    app.SeedingDB();

}
if (app.Environment.IsProduction())
{
    app.SeedingDB();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapCarter();
app.Run();
