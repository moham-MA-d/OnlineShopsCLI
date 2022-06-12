using System.Text.Json.Serialization;

namespace CLI.Responses
{
    public record ProductTypeResponse
    {
        [JsonPropertyName("name")]
        public string Name{get;set;}
    }
}