using SchoolManagement.Domain.Users;

namespace SchoolManagement.Application.Authentication.Common
{
    public record AuthenticationResult(User User, string Token);
}