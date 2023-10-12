using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Schools;
using SchoolManagement.Domain.Schools.Enums;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Schools.Update
{
    public class UpdateSchoolCommandHandler : IRequestHandler<UpdateSchoolCommand, ErrorOr<School>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSchoolCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<School>> Handle(UpdateSchoolCommand command, CancellationToken cancellationToken)
        {
            var school = await _unitOfWork.SchoolRepository.GetByIdAsync(Guid.Parse(command.SchoolId));

            if (school is null)
            {
                return Errors.School.NotFound;
            }

            // Convert the string SchoolType to enum
            if (!Enum.TryParse(command.SchoolType, out SchoolType schoolType))
            {
                return Errors.School.InvalidSchoolType;
            }

            if (school.Name != command.Name)
            {
                var schoolReponse = await _unitOfWork.SchoolRepository.GetSchoolByNameAsync(command.Name);
                if (schoolReponse is not null)
                {
                    return Errors.School.DuplicateSchoolName;
                }
            }

            school.Update(name: command.Name, schoolType: schoolType);

            await _unitOfWork.SchoolRepository.UpdateAsync(school);
            await _unitOfWork.CompleteAsync();

            return school;
        }
    }
}