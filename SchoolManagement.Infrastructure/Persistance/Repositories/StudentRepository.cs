using ErrorOr;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Students;
using SchoolManagement.Domain.Students.ValueObjects;

namespace SchoolManagement.Infrastructure.Persistance.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async override Task<List<Student>> AllAsync()
        {
            return await _context.Students
                .Include(x => x.StudentLessons)
                    .ThenInclude(x => x.Lesson)
                .Include(x => x.School)
                .ToListAsync();
        }

        public override async Task<Student?> GetByIdAsync(Guid id)
        {
            var studentId = new StudentId(id);
            return await _context.Students
                .Include(x => x.StudentLessons)
                    .ThenInclude(x => x.Lesson)
                .Include(x => x.School)
                .FirstOrDefaultAsync(x => x.StudentId == studentId);
        }

    }
}