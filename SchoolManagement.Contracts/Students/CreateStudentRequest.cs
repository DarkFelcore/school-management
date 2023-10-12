namespace SchoolManagement.Contracts.Students
{
    public record CreateStudentRequest(
        string FirstName,
        string LastName, 
        string SchoolName
    );
}