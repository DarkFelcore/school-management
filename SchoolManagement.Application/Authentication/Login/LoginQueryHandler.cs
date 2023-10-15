using ErrorOr;
using MediatR;
using SchoolManagement.Application.Authentication.Common;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Application.Authentication.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginQueryHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            // Validate the user
            if (await _unitOfWork.UserRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.User.InvalidCredentials;
            }

            // Validate the password
            if (!BCrypt.Net.BCrypt.Verify(query.Password, user.PasswordHash))
            {
                return Errors.User.InvalidCredentials; ;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}