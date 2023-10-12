using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Application.Schools.Delete
{
    public class DeleteSchoolCommandHandler : IRequestHandler<DeleteSchoolCommand, ErrorOr<School>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSchoolCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<School>> Handle(DeleteSchoolCommand command, CancellationToken cancellationToken)
        {
            var school = await _unitOfWork.SchoolRepository.GetByIdAsync(Guid.Parse(command.SchoolId));

            if (school is null)
            {
                return Errors.School.NotFound;
            }

            // If there are any students in the school, prevent deletion
            if (school.Students.Count > 0)
            {
                return Errors.School.SchoolContainsStudents;
            }

            await _unitOfWork.SchoolRepository.DeleteAsync(school);
            await _unitOfWork.CompleteAsync();

            return school;
        }
    }
}