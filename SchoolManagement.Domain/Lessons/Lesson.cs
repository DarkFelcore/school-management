using SchoolManagement.Domain.Common.JoinedEntities;
using SchoolManagement.Domain.Lessons.Enums;
using SchoolManagement.Domain.Lessons.ValueObjects;

namespace SchoolManagement.Domain.Lessons
{
    public class Lesson
    {
        public LessonId LessonId { get; set; }
        public LessonSubject Subject { get; set; }

        // Many to many with Student
        public List<StudentLessons> StudentLessons { get; set; }
        public Lesson()
        {
        }
        public Lesson(LessonSubject subject)
        {
            LessonId = new LessonId(Guid.NewGuid());
            Subject = subject;
        }

        public void Update(LessonSubject subject)
        {
            Subject = subject;
        }

    }
}