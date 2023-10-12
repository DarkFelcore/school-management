namespace SchoolManagement.Contracts.Students
{
    public record UpdateStudentRequest(
        string FirstName,
        string LastName,
        string SchoolName, 
        List<string> Lessons
    );
}