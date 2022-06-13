using System.Collections.Generic;

namespace CLI.Models;

public record OnlineShopSearchResult
{
    public IReadOnlyList<OnlineShopResult> OnlineShops {get;init;}
}