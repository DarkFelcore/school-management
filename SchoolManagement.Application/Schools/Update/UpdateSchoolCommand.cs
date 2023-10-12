using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Application.Schools.Update
{
    public record UpdateSchoolCommand(
        string SchoolId,
        string Name,
        string SchoolType
    ): IRequest<ErrorOr<School>>;
}