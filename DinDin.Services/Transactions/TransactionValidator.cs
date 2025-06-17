using DinDin.Domain.Constantes;
using DinDin.Domain.Transactions;
using DinDin.Domain.Transactions.Enums;
using FluentValidation;
namespace DinDin.Services.Transactions 
{ 
    public class TransactionValidator : AbstractValidator<Transaction> 
    { 
        public TransactionValidator() 
        { 
            ClassLevelCascadeMode = CascadeMode.Stop;
            
            RuleSet(ApplicationConstants.TRANSACTION_CREATE_RULE_SET_NAME, () =>
            { 
                RuleFor(transaction => transaction.Type)
                    .NotEmpty().WithMessage("O campo tipo é obrigatório")
                    .IsInEnum().WithMessage("Esse valor não é válido para o enum");
                
                RuleFor(transaction => transaction.Amont)
                    .NotEmpty().WithMessage("O campo valor é obrigatório");
                
                RuleFor(transaction => transaction.Description)
                    .NotEmpty().WithMessage("O campo descrição é obrigatório");
                
                RuleFor(transaction => transaction.IncomeCategory)
                    .NotEmpty()
                    .When(transaction => transaction.Type == TransactionType.Income)
                    .IsInEnum().WithMessage("Esse valor não é válido para o enum");
                
                RuleFor(transaction => transaction.ExpenseCategory)
                    .NotEmpty()
                    .When(transaction => transaction.Type == TransactionType.Expense)
                    .IsInEnum().WithMessage("Esse valor não é válido para o enum");
            });
        } 
    } 
}