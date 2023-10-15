namespace SchoolManagement.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        ISchoolRepository SchoolRepository { get; }
        ILessonRepository LessonRepository { get; }
        IUserRepository UserRepository { get; }
        Task CompleteAsync();
    }
}