using ErrorOr;

namespace SchoolManagement.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Student
        {
            public static Error NotFound => Error.NotFound
            (
                code: "Student.NotFound",
                description: "Student not found"
            );
        }
    }
}