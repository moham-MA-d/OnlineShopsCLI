using System;
using System.Collections.Generic;
namespace CLI.Models;

public record OnlineShopResult
{
    public string Name { get; init; }
    public double Rating { get; init; }
    public DateTime Date { get; init; }
    public IReadOnlyList<string> ProdoctTypes { get; init; }
}