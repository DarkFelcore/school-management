using SchoolManagement.Domain.Users;

namespace SchoolManagement.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}