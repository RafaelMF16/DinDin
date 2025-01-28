﻿using DinDin.Domain.Users;
using FluentValidation;

namespace DinDin.Services.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(user => user.Name)
                .NotEmpty().WithMessage("The Name field is mandatory")
                .MaximumLength(100).WithMessage("The Name field can contain a maximum of 100 characters");

            RuleFor(user => user.Login)
                .NotEmpty().WithMessage("The Login field is mandatory")
                .MaximumLength(100).WithMessage("The Login field can contain a maximum of 100 characters");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("The Password field is mandatory")
                .MinimumLength(8).WithMessage("The Password field can contain a minimum of 8 characters")
                .MaximumLength(50).WithMessage("The Password field can contain a maximum of 50 characters");

            RuleFor(user => user)
                .Must(user => ValidateUserCreationDate(user)).WithMessage("The Creation Date not is valid");
        }

        private bool ValidateUserCreationDate(User user)
        {
            const int idNull = 0;

            if (user.Id == idNull)
                return user.CreationDate.Date == DateTime.UtcNow.Date;

            var dataBaseUser = _userRepository.GetById(user.Id)
                ?? throw new ArgumentNullException($"Not find user with id: {user.Id}");

            return user.CreationDate.Date == dataBaseUser.CreationDate.Date;
        }
    }
}