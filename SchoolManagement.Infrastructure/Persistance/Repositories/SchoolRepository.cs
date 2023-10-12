using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Schools;
using SchoolManagement.Domain.Schools.ValueObjects;

namespace SchoolManagement.Infrastructure.Persistance.Repositories
{
    public class SchoolRepository : GenericRepository<School>, ISchoolRepository
    {
        public SchoolRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<List<School>> AllAsync()
        {
            return await _context.Schools
                .Include(x => x.Students)
                .ToListAsync();
        }

        public override async Task<School?> GetByIdAsync(Guid id)
        {
            var schoolId = new SchoolId(id);
            return await _context.Schools.Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.SchoolId == schoolId);
        }

        public async Task<School?> GetSchoolByNameAsync(string name)
        {
            return await _context.Schools.FirstOrDefaultAsync(school => school.Name == name);
        }
    }
}