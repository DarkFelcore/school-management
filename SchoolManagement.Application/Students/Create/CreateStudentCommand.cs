using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Create
{
    public record CreateStudentCommand(
        string FirstName,
        string LastName,
        string SchoolName
    ) : IRequest<ErrorOr<Student>>;
}