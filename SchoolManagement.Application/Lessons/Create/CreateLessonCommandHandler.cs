using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Lessons;
using SchoolManagement.Domain.Lessons.Enums;

namespace SchoolManagement.Application.Lessons.Create
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, ErrorOr<Lesson>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLessonCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Lesson>> Handle(CreateLessonCommand command, CancellationToken cancellationToken)
        {
            // Convert the string SchoolType to enum
            if (!Enum.TryParse(command.Subject, out LessonSubject subject))
            {
                return Errors.Lesson.InvalidSubject;
            }

            // Check if the subject already exists
            var lessonReponse = await _unitOfWork.LessonRepository.GetLessonByNameAsync(command.Subject);
            if(lessonReponse is not null)
            {
                return Errors.Lesson.DuplicateSubject;
            }

            var lesson = new Lesson(
                subject: subject
            );

            await _unitOfWork.LessonRepository.AddAsync(lesson);
            await _unitOfWork.CompleteAsync();

            return lesson;
        }
    }
}