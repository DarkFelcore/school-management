using SchoolManagement.Domain.Schools.Enums;
using SchoolManagement.Domain.Schools.ValueObjects;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Domain.Schools
{
    public class School
    {

        public SchoolId SchoolId { get; set; }
        public string Name { get; set; }
        public SchoolType SchoolType { get; set; }

        // One to many with Student
        public List<Student> Students { get; set; }
        public School()
        {
        }
        public School(string name, SchoolType schoolType)
        {
            SchoolId = new SchoolId(Guid.NewGuid());
            Name = name;
            SchoolType = schoolType;
            Students = new List<Student>();
        }


        public void Update(string name, SchoolType schoolType)
        {
            Name = name;
            SchoolType = schoolType;
        }
    }
}
