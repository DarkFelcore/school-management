using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Lessons;

namespace SchoolManagement.Application.Lessons.Delete
{
    public record DeleteLessonCommand(
        string LessonId
    ) : IRequest<ErrorOr<Lesson>>;
}