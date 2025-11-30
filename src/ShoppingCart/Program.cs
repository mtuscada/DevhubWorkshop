using ShoppingCart.DataSourcing;
using ShoppingCart.Migration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions<ConnectionOptions>();

var connectionStringsSection = builder.Configuration.GetSection(nameof(ConnectionOptions));
builder.Services.AddOptions<ConnectionOptions>()
    .Bind(connectionStringsSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();
var options = new ConnectionOptions();
connectionStringsSection.Bind(options);

builder.Services.AddSingleton<IDbUpLogger, DbUpLogger>();
builder.Services.AddSingleton<IStartupFilter, DbMigration>();
builder.Services.AddSingleton<IRepository, Repository>();
builder.Services.AddSingleton<IMemoryCachingService, MemoryCachingService>();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();


app.UseHttpsRedirection();

app.Run();

public partial class Program { }
