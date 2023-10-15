using ErrorOr;
using MediatR;
using SchoolManagement.Application.Authentication.Common;

namespace SchoolManagement.Application.Authentication.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        bool? IsAdmin
    ): IRequest<ErrorOr<AuthenticationResult>>;
}