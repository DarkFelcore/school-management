namespace SchoolManagement.Contracts.Schools
{
    public record CreateSchoolRequest(
        string Name,
        string SchoolType
    );
}