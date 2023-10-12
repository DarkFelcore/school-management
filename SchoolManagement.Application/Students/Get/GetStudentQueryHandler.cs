using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Get
{
    public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, ErrorOr<Student>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStudentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Student>> Handle(GetStudentQuery query, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(Guid.Parse(query.StudentId));

            if (student is null)
            {
                return Errors.Student.NotFound;
            }

            return student;
        }
    }
}