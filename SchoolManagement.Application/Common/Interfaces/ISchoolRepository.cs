using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Application.Common.Interfaces
{
    public interface ISchoolRepository : IGenericRepository<School>
    {
        Task<School?> GetSchoolByNameAsync(string name);
    }
}