using JsonFlatFileDataStore;
using Rover.Api.Services;
using Rover.Core.Services;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<SimulationService>();
builder.Services.AddSingleton<DataStoreService>();

IWebHostEnvironment env = builder.Environment;
builder.Services.AddSingleton(_ => new DataStore(Path.Combine(env.ContentRootPath, "Data", "simulations.json")));

builder.Services.AddControllers()
    // JsonOption to handle deserialization of direction to Enum from string instead of int
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
