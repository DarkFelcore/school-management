using SchoolManagement.Contracts.Students;

namespace SchoolManagement.Contracts.Schools
{
    public record SchoolResponse(
        Guid Id,
        string Name,
        string SchoolType,
        List<string> Students
    );
}