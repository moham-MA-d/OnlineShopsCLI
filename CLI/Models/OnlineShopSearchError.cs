using System.Collections.Generic;

namespace CLI.Models;

public class OnlineShopSearchError
{
    public OnlineShopSearchError(IReadOnlyList<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    public IReadOnlyList<string> ErrorMessages { get; init; }
}