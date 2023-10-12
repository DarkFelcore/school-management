using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Schools.ValueObjects;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Create
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, ErrorOr<Student>>
    {
        private IUnitOfWork _unitOfWork;
        public CreateStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Student>> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var school = await _unitOfWork.SchoolRepository.GetSchoolByNameAsync(command.SchoolName);

            if (school is null)
            {
                return Errors.School.NotFound;
            }

            var student = new Student
            (
                firstName: command.FirstName,
                lastName: command.LastName,
                schoolId: new SchoolId(school.SchoolId.Value)
            );

            await _unitOfWork.StudentRepository.AddAsync(student);
            await _unitOfWork.CompleteAsync();

            return student;
        }
    }
}