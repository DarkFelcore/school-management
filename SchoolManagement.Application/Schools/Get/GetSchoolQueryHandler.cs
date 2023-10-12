using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Application.Schools.Get
{
    public class GetSchoolQueryHandler : IRequestHandler<GetSchoolQuery, ErrorOr<School>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetSchoolQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<School>> Handle(GetSchoolQuery query, CancellationToken cancellationToken)
        {
            var school = await _unitOfWork.SchoolRepository.GetByIdAsync(Guid.Parse(query.StudentId));

            if(school is null)
            {
                return Errors.School.NotFound;
            }

            return school;
        }
    }
}