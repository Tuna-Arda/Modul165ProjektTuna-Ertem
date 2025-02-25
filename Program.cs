using JetstreamBackend.Data;
using JetstreamBackend.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// MongoDB-Konfiguration
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<OrderService>();

// SQL Server-Konfiguration
builder.Services.Configure<SqlServerSettings>(builder.Configuration.GetSection("SqlServerSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<SqlServerSettings>>().Value);
builder.Services.AddSingleton<SqlServerService>(sp =>
    new SqlServerService(sp.GetRequiredService<SqlServerSettings>().ConnectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Statische Dateien bereitstellen
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
