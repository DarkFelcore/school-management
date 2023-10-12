namespace SchoolManagement.Contracts.Schools
{
    public record UpdateSchoolRequest(
        string Name,
        string SchoolType
    );
}