﻿using DinDin.Domain.Users;
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
                .NotEmpty().WithMessage("The Name field is mandatory")
                .MaximumLength(100).WithMessage("The Name field can contain a maximum of 100 characters");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("The Email field is mandatory")
                .EmailAddress().WithMessage("Email Invalid")
                .MustAsync(ValidateIfEmailHasAlreadyBeenRegistered).WithMessage("Email has already regitered")
                .MaximumLength(100).WithMessage("The Email field can contain a maximum of 100 characters");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("The Password field is mandatory")
                .MinimumLength(8).WithMessage("The Password field can contain a minimum of 8 characters")
                .MaximumLength(50).WithMessage("The Password field can contain a maximum of 50 characters");

            RuleFor(user => user)
                .Must(user => ValidateUserCreationDate(user)).WithMessage("The Creation Date not is valid");
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