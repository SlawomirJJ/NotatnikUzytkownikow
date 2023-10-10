using FluentValidation;
using NotatnikUzytkownikow.Requests;

namespace NotatnikUzytkownikow.Validators
{
    public class AdditionalAttributeRequestValidator : AbstractValidator<AdditionalAttributeRequest>
    {
        public AdditionalAttributeRequestValidator()
        {
            RuleFor(x => x.AttributeName)
                .NotEmpty()
                .MaximumLength(50)
                .WithErrorCode("invalid_attribute_name");

            RuleFor(x => x.Value)
                .NotEmpty()
                .MaximumLength(150)
                .WithErrorCode("invalid_value");
        }
    }
}
