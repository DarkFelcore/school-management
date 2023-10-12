using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Get
{
    public record GetStudentQuery(string StudentId) : IRequest<ErrorOr<Student>>;
}