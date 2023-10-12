using SchoolManagement.Application.Common.Interfaces;

namespace SchoolManagement.Infrastructure.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IStudentRepository StudentRepository { get; private set; }
        public ISchoolRepository SchoolRepository { get; private set; }

        public ILessonRepository LessonRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            StudentRepository =  new StudentRepository(_context);
            SchoolRepository =  new SchoolRepository(_context);
            LessonRepository = new LessonRepository(_context);
        }


        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}