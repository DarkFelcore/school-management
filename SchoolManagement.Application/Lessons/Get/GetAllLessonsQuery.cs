using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Lessons;

namespace SchoolManagement.Application.Lessons.Get
{
    public record GetAllLessonsQuery() : IRequest<ErrorOr<List<Lesson>>>;
}