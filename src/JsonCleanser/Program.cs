using System.Text.Json;
using JsonCleanser.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddSingleton<IJsonCleanserService, JsonCleanserService>()
	.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	});

var app = builder.Build();

app.MapControllers();

app.Run();