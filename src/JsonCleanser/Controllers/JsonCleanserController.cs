using System.Text.Json;
using JsonCleanser.Services;
using Microsoft.AspNetCore.Mvc;

namespace JsonCleanser.Controllers;

[ApiController]
public class JsonCleanserController
{
	private readonly IJsonCleanserService _jsonCleanserService;

	public JsonCleanserController(IJsonCleanserService jsonCleanserService)
	{
		_jsonCleanserService = jsonCleanserService;
	}
	
	[HttpPost("/json/clean")]
	public IActionResult CleanJson([FromBody] JsonDocument jsonDocument)
	{
		var nodes = _jsonCleanserService.Process(jsonDocument.RootElement);
		
		var json = JsonSerializer.Serialize(nodes);

		return new ContentResult
		{
			StatusCode = 200,
			ContentType = "application/json",
			Content = json
		};
	}
}