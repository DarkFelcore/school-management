using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        ISchoolRepository SchoolRepository { get; }
        ILessonRepository LessonRepository { get; }
        Task CompleteAsync();
    }
}