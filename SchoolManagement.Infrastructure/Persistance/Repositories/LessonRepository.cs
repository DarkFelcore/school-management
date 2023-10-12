using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Lessons;
using SchoolManagement.Domain.Lessons.Enums;
using SchoolManagement.Domain.Lessons.ValueObjects;

namespace SchoolManagement.Infrastructure.Persistance.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<List<Lesson>> AllAsync()
        {
            return await _context.Lessons
                .Include(x => x.StudentLessons)
                    .ThenInclude(x => x.Student)
                .ToListAsync();
        }

        public override async Task<Lesson?> GetByIdAsync(Guid id)
        {
            var lessonId = new LessonId(id);
            return await _context.Lessons
                .Include(x => x.StudentLessons)
                    .ThenInclude(x => x.Student)
                .FirstOrDefaultAsync(x => x.LessonId == lessonId);
        }

        public async Task<Lesson?> GetLessonByNameAsync(string name)
        {
            // Load all lessons from the database
            var lessons = await _context.Lessons.ToListAsync();

            // Parse the input name to LessonSubject enum
            if (Enum.TryParse<LessonSubject>(name, true, out var subject))
            {
                // Filter the lessons in memory based on the parsed subject
                return lessons.FirstOrDefault(x => x.Subject == subject);
            }

            return null;
        }
    }
}