using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Application.Schools.Delete
{
    public record DeleteSchoolCommand(
        string SchoolId
    ) : IRequest<ErrorOr<School>>;
}