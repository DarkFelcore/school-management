using ErrorOr;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Common.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
    }
}