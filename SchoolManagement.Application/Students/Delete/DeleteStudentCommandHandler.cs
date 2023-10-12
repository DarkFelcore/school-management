using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Delete
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, ErrorOr<Student>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Student>> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(Guid.Parse(command.StudentId));

            if(student is null)
            {
                return Errors.Student.NotFound;
            }

            await _unitOfWork.StudentRepository.DeleteAsync(student);
            await _unitOfWork.CompleteAsync();

            return student;
        }
    }
}