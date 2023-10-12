using SchoolManagement.Domain.Lessons;

namespace SchoolManagement.Application.Common.Interfaces
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<Lesson?> GetLessonByNameAsync(string name);
    }
}