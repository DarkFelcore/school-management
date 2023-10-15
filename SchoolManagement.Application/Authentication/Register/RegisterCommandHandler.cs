using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using ErrorOr;
using MediatR;
using SchoolManagement.Application.Authentication.Common;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Users;
using SchoolManagement.Domain.Users.ValueObjects;

namespace SchoolManagement.Application.Authentication.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            // Check if user is already registered
            if ((await _unitOfWork.UserRepository.GetUserByEmail(command.Email)) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            // Hash user password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(command.Password);

            // Creating new user object
            var newUser = new User
            {
                UserId = new UserId(Guid.NewGuid()),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                PasswordHash = passwordHash,
                IsAdmin = command.IsAdmin ?? false
            };

            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();

            var token = _jwtTokenGenerator.GenerateToken(newUser);

            return new AuthenticationResult(newUser, token);
        }
    }
}