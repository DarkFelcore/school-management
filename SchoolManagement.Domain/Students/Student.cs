using SchoolManagement.Domain.Common.JoinedEntities;
using SchoolManagement.Domain.Schools;
using SchoolManagement.Domain.Schools.ValueObjects;
using SchoolManagement.Domain.Students.ValueObjects;

namespace SchoolManagement.Domain.Students
{
    public class Student
    {
        public StudentId StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // One to many with School
        public SchoolId SchoolId { get; set; }
        public School School { get; set; }

        // Many to many with Lesson
        public List<StudentLessons> StudentLessons { get; set; }
        public Student()
        {
        }
        public Student(string firstName, string lastName, SchoolId schoolId)
        {
            StudentId = new StudentId(Guid.NewGuid());
            FirstName = firstName;
            LastName = lastName;
            SchoolId = schoolId;
        }

    }
}
