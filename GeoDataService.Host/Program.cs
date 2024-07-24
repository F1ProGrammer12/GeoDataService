using GeoDataService.Host.Middlewares;
using GeoDataService.Logic.Interfaces;
using GeoDataService.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpLogging(o => { });

builder.Services.AddTransient<IGeoDataService, GeographicDataService>();

builder.Services.AddHttpClient();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandling();

app.MapControllers();

app.Run();
