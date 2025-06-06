using DinDin.Domain.Constantes;
using DinDin.Domain.Users;
using FluentValidation;

namespace DinDin.Services.Users
{
    public class ValidatorUser : AbstractValidator<User>
    {
        public ValidatorUser()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleSet(ApplicationConstants.USER_CREATE_RULE_SET_NAME, () =>
            {
                RuleFor(user => user.Name)
                .NotEmpty().WithMessage("The Name field is mandatory")
                .MaximumLength(100).WithMessage("The Name field can contain a maximum of 100 characters");

                RuleFor(user => user.Email)
                    .NotEmpty().WithMessage("The Email field is mandatory")
                    .EmailAddress().WithMessage("Email Invalid")
                    .MaximumLength(100).WithMessage("The Email field can contain a maximum of 100 characters");

                RuleFor(user => user.Password)
                    .NotEmpty().WithMessage("The Password field is mandatory")
                    .MinimumLength(8).WithMessage("The Password field can contain a minimum of 8 characters")
                    .MaximumLength(50).WithMessage("The Password field can contain a maximum of 50 characters");

                RuleFor(user => user.CreationDate)
                    .Must(date => date.Day == DateTime.UtcNow.Day).WithMessage("The Creation Date not is valid");
            });

            RuleSet(ApplicationConstants.USER_UPDATE_RULE_SET_NAME, () =>
            {
                RuleFor(user => user.Name)
                    .NotEmpty().WithMessage("The Name field is mandatory")
                    .MaximumLength(100).WithMessage("The Name field can contain a maximum of 100 characters");

                RuleFor(user => user.Password)
                    .NotEmpty().WithMessage("The Password field is mandatory")
                    .MinimumLength(8).WithMessage("The Password field can contain a minimum of 8 characters")
                    .MaximumLength(50).WithMessage("The Password field can contain a maximum of 50 characters");
            });
        }
    }
}