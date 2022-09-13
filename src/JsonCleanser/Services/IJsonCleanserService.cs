using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonCleanser.Services;

public interface IJsonCleanserService
{
	public JsonNode? Process(JsonElement jsonElement);
}