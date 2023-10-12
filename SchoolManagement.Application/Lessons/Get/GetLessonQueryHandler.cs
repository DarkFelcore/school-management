using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Lessons;

namespace SchoolManagement.Application.Lessons.Get
{
    public class GetLessonQueryHandler : IRequestHandler<GetLessonQuery, ErrorOr<Lesson>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetLessonQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Lesson>> Handle(GetLessonQuery query, CancellationToken cancellationToken)
        {
            var lesson = await _unitOfWork.LessonRepository.GetByIdAsync(Guid.Parse(query.LessonId));

            if(lesson is null)
            {
                return Errors.Lesson.NotFound;
            }

            return lesson;
        }
    }
}