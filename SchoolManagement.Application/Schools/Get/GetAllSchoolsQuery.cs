using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Application.Schools.Get
{
    public record GetAllSchoolsQuery() : IRequest<ErrorOr<List<School>>>;
}