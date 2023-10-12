using SchoolManagement.Contracts.Students;

namespace SchoolManagement.Contracts.Lessons
{
    public record LessonResponse(
        Guid Id,
        string Subject,
        List<string> Students
    );
}