using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Get
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, ErrorOr<List<Student>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllStudentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<Student>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _unitOfWork.StudentRepository.AllAsync();

            if (students is null)
            {
                return Errors.Student.NotFound;
            }

            return students;
        }
    }
}