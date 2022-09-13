using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonCleanser.Services;

public class JsonCleanserService : IJsonCleanserService
{
	private readonly HashSet<string> _invalidValues = new HashSet<string> { "N/A", "", "-" };
	
	public JsonNode? Process(JsonElement jsonElement)
	{
		switch (jsonElement.ValueKind)
		{
			case JsonValueKind.Object:
				var newObj = new JsonObject();
		
				foreach (var property in jsonElement.EnumerateObject())
				{
					var node = Process(property.Value);

					if (node is not null)
					{
						newObj.Add(new KeyValuePair<string, JsonNode?>(property.Name, node));
					}
				}

				return newObj;
			case JsonValueKind.Array:
				var items = new JsonArray();
		
				foreach (var item in jsonElement.EnumerateArray())
				{
					var results = Process(item);
			
					if (results is not null)
					{
						items.Add(results);
					}
				}

				return items;
			case JsonValueKind.String:
				var str = jsonElement.GetString()!;

				return _invalidValues.Contains(str) ? null : str;
			case JsonValueKind.Number:
				return jsonElement.GetDouble();
			case JsonValueKind.True:
			case JsonValueKind.False:
				return jsonElement.GetBoolean();
			case JsonValueKind.Undefined:
			case JsonValueKind.Null:
				return null;
			default:
				throw new ArgumentOutOfRangeException(nameof(jsonElement), "Element is not a valid kind.");
		}
	}
}