namespace SchoolManagement.Contracts.Students
{
    public record StudentResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string SchoolName,
        List<string> LessonNames
    );
}