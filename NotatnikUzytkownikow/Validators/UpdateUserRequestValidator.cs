using FluentValidation;
using NotatnikUzytkownikow.Requests;

namespace NotatnikUzytkownikow.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50)
                .WithErrorCode("invalid_first_name");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(150)
                .WithErrorCode("invalid_last_name");

            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .Must(BeValidBirthDate)
                .WithErrorCode("invalid_birth_date");

            RuleFor(x => x.Gender)
                .IsInEnum()
                .WithErrorCode("invalid_gender");
        }

        private bool BeValidBirthDate(DateOnly birthDate)
        {
            DateOnly todayDateOnly = DateOnly.FromDateTime(DateTime.Today);

            int userAge = todayDateOnly.Year - birthDate.Year;
            int maxAge = 122;

            if (birthDate <= todayDateOnly && userAge <= maxAge)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
