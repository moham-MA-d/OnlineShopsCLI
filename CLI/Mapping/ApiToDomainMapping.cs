using System.Linq;
using CLI.Models;
using CLI.Responses;

namespace CLI.Mapping;

public static class ContractToModelMapping
{
    public static OnlineShopResult ToOnlineShopResult(this OnlineShopResponse response)
    {
        return new OnlineShopResult
        {
            Name = response.Name,
            Rating = response.Rating,
            ProdoctTypes = response.ProductTypes.Select(x=>x.Name).ToList()
        };
    }
}