using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Lessons;

namespace SchoolManagement.Application.Lessons.Get
{
    public record GetLessonQuery(string LessonId) : IRequest<ErrorOr<Lesson>>;
}