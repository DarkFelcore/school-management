using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Lessons;

namespace SchoolManagement.Application.Lessons.Delete
{
    public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand, ErrorOr<Lesson>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Lesson>> Handle(DeleteLessonCommand command, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.LessonRepository.GetByIdAsync(Guid.Parse(command.LessonId));

            if (lesson is null)
            {
                return Errors.Lesson.NotFound;
            }

            await _unitOfWork.LessonRepository.DeleteAsync(lesson);
            await _unitOfWork.CompleteAsync();

            return lesson;
        }
    }
}