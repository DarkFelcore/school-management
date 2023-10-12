using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Schools;
using SchoolManagement.Domain.Schools.Enums;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Schools.Create
{
    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, ErrorOr<School>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateSchoolCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<School>> Handle(CreateSchoolCommand command, CancellationToken cancellationToken)
        {
            // Convert the string SchoolType to enum
            if (!Enum.TryParse(command.SchoolType, out SchoolType schoolType))
            {
                return Errors.School.InvalidSchoolType;
            }

            var schoolReponse = await _unitOfWork.SchoolRepository.GetSchoolByNameAsync(command.Name);
            if (schoolReponse is not null)
            {
                return Errors.School.DuplicateSchoolName;
            }

            var school = new School(
                name: command.Name,
                schoolType: schoolType
            );

            await _unitOfWork.SchoolRepository.AddAsync(school);
            await _unitOfWork.CompleteAsync();

            return school;
        }
    }
}