using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Update
{
    public record UpdateStudentCommand(
        string StudentId,
        string FirstName,
        string LastName,
        string SchoolName,
        List<string> Lessons
    ) : IRequest<ErrorOr<Student>>;
}