using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Application.Schools.Create
{
    public record CreateSchoolCommand(
        string Name,
        string SchoolType
    ): IRequest<ErrorOr<School>>;
}