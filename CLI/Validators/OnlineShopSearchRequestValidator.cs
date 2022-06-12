using CLI.Models;
using FluentValidation;

namespace CLI.Validators
{
    public class OnlineShopSearchRequestValidator : AbstractValidator<OnlineShopSearchRequest>
    {
        public OnlineShopSearchRequestValidator()
        {
            RuleFor(request => request.Outcode)
                .Matches("^([A-Za-z][0-9]{1,2})$")
                .WithMessage ("Please provide a valid UK Outcode");
        }
    }
}