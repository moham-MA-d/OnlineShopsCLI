using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CLI.Responses
{
        public record OnlineShopSearchResponse
        {
            [JsonPropertyName("products")] // to make sure everything serialize and deserialize properly
            public IReadOnlyList<OnlineShopResponse> Products {get; init;}
        }
}