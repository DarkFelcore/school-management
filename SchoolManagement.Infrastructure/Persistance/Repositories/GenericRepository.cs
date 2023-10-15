using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.Common.Interfaces;

namespace SchoolManagement.Infrastructure.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected ApplicationDbContext _context;
        internal DbSet<T> DbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public async Task<bool> AddAsync(T item)
        {
            await DbSet.AddAsync(item);
            return true;
        }

        public virtual async Task<List<T>> AllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<bool> DeleteAsync(T item)
        {
            DbSet.Remove(item);
            return true;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T item)
        {
            DbSet.Update(item);
            return true;
        }
    }
}