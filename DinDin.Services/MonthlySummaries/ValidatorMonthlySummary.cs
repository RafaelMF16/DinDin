using DinDin.Domain.MonthlySummaries;
using FluentValidation;

namespace DinDin.Services.MonthlySummaries
{
    public class ValidatorMonthlySummary : AbstractValidator<MonthlySummary>
    {
        public ValidatorMonthlySummary()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(monthlySummary => monthlySummary.Year)
                .NotEmpty().WithMessage("The Year field is mandatory")
                .Must(year => year == DateTime.UtcNow.Year).WithMessage("The Year field must be equal to the current year");

            RuleFor(monthlySummary => monthlySummary.Month)
                .NotEmpty().WithMessage("The Month field is mandatory")
                .Must(month => month == DateTime.UtcNow.Month).WithMessage("The Month field must be equal to the current month");

            RuleFor(monthlySummary => monthlySummary.TotalIncome)
                .NotNull().WithMessage("The TotalIncome field is mandatory");

            RuleFor(monthlySummary => monthlySummary.TotalExpense)
                .NotEmpty().WithMessage("The TotalExpense field is mandatory");

            RuleFor(monthlySummary => monthlySummary.Balance)
                .NotEmpty().WithMessage("The Balance field is mandatory");
        }
    }
}