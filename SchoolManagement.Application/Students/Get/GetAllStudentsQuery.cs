using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Get
{
    public record GetAllStudentsQuery() : IRequest<ErrorOr<List<Student>>>;
}