using DinDin.Domain.Users;
using FluentValidation;

namespace DinDin.Services.Users
{
    public class ValidatorUser : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;

        public ValidatorUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("O campo Nome é obrigatório")
                .MaximumLength(100).WithMessage("O campo Nome pode conter um máximo de 100 caracteres");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatório")
                .EmailAddress().WithMessage("E-mail inválido")
                .MustAsync(ValidateIfEmailHasAlreadyBeenRegistered).WithMessage("O e-mail já foi registrado")
                .MaximumLength(100).WithMessage("O campo E-mail pode conter um máximo de 100 caracteres");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("O campo Senha é obrigatório")
                .MinimumLength(8).WithMessage("O campo Senha pode conter um mínimo de 8 caracteres")
                .MaximumLength(50).WithMessage("O campo Senha pode conter um máximo de 50 caracteres");

            RuleFor(user => user)
                .Must(user => ValidateUserCreationDate(user)).WithMessage("A data de criação não é válida");
        }

        private async Task<bool> ValidateIfEmailHasAlreadyBeenRegistered(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user == null;
        }

        private bool ValidateUserCreationDate(User user)
        {
            if (string.IsNullOrEmpty(user.Id))
                return user.CreationDate.Date == DateTime.UtcNow.Date;

            var dataBaseUser = _userRepository.GetById(user.Id)
                ?? throw new ArgumentNullException($"Not find user with id: {user.Id}");

            return user.CreationDate.Date == dataBaseUser.CreationDate.Date;
        }
    }
}