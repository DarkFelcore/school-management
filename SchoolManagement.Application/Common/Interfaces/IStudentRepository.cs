using ErrorOr;
using SchoolManagement.Domain.Students;
using SchoolManagement.Domain.Students.ValueObjects;

namespace SchoolManagement.Application.Common.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
    }
}