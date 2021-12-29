using CarRental.API.Models.Requests;
using FluentValidation;

namespace CarRental.API.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(f => f.FirstName)
                .NotEmpty().WithMessage("First Name should be not empty.")
                .Length(2, 32);

            RuleFor(l => l.LastName)
                .NotEmpty().WithMessage("Last Name should be not empty.")
                .Length(2, 64);

            RuleFor(e => e.Email)
                .EmailAddress();

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password should be not empty.")
                .Length(8, 64);
        }
    }
}
