using CarRental.API.Models.Requests;
using FluentValidation;

namespace CarRental.API.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email should be not empty.")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password should be not empty.");
        }
    }
}
