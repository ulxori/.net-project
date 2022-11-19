using api.Filters;
using api.Filters.Measurement;
using api.Models;
using api.Repositories;
using api.Services;
using api.Settings;
using api.Workers;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddSingleton<IMeasurementService, MeasurementService>();
builder.Services.AddSingleton<MongoSettings>();
builder.Services.AddSingleton<IMeasurementFilteringArgResolver, MeasurementFilterArgResolver>();
builder.Services.AddSingleton<IMeasurementSortingArgResolver, MeasurementSortingArgResolver>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
