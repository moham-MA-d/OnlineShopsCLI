using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CLI.Responses
{
    public record OnlineShopResponse
    {
        [JsonPropertyName("name")] 
        public string Name { get; init; }
        [JsonPropertyName("ratingStars")] 
        public double Rating { get; init; }
        [JsonPropertyName("productTypes")] 
        public IReadOnlyList<ProductTypeResponse> ProductTypes { get; set; }
    }
}