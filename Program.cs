using JetstreamBackend.Data;
using JetstreamBackend.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using JetstreamBackend.Data;
using JetstreamBackend.Services;


var builder = WebApplication.CreateBuilder(args);

// MongoDB-Konfiguration
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<OrderService>();

// MariaDB-Konfiguration
builder.Services.Configure<MariaDbSettings>(builder.Configuration.GetSection("MariaDbSettings"));
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MariaDbSettings>>().Value);
builder.Services.AddSingleton<MariaDbService>(sp =>
    new MariaDbService(sp.GetRequiredService<MariaDbSettings>().ConnectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Statische Dateien aus wwwroot bereitstellen
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
