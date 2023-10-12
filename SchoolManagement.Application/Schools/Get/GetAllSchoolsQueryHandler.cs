using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Application.Schools.Get
{
    public class GetAllSchoolsQueryHandler : IRequestHandler<GetAllSchoolsQuery, ErrorOr<List<School>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllSchoolsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<School>>> Handle(GetAllSchoolsQuery request, CancellationToken cancellationToken)
        {
            var schools = await _unitOfWork.SchoolRepository.AllAsync();

            if (schools is null)
            {
                return Errors.School.NotFound;
            }

            return schools;
        }
    }
}