using SchoolManagement.Domain.Lessons;
using SchoolManagement.Domain.Lessons.ValueObjects;
using SchoolManagement.Domain.Students;
using SchoolManagement.Domain.Students.ValueObjects;

namespace SchoolManagement.Domain.Common.JoinedEntities
{
    public class StudentLessons
    {
        public StudentId StudentId { get; set; }
        public Student Student { get; set; }

        public LessonId LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
