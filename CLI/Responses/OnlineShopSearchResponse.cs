using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CLI.Responses
{
        public record OnlineShopSearchResponse
        {
            [JsonPropertyName("products")] 
            public IReadOnlyList<OnlineShopResponse> Products {get; init;}
        }
}