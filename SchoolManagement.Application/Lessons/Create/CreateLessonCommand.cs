using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Lessons;

namespace SchoolManagement.Application.Lessons.Create
{
    public record CreateLessonCommand(
        string LessonId,
        string Subject
    ): IRequest<ErrorOr<Lesson>>;
}