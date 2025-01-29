using DinDin.Domain.Acconts;
using FluentValidation;

namespace DinDin.Services.Acconts
{
    public class ValidatorAccont : AbstractValidator<Accont>
    {
        public ValidatorAccont()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(accont => accont.UserId)
                .NotEmpty().WithMessage("The UserId field is mandatory");
        }
    }
}