using ErrorOr;

namespace SchoolManagement.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class School
        {
            public static Error NotFound => Error.NotFound
            (
                code: "School.NotFound",
                description: "School not found"
            );

            public static Error DuplicateSchoolName => Error.Validation
            (
                code: "School.DuplicateSchoolName",
                description: "School name already exists"
            );

            public static Error InvalidSchoolType => Error.NotFound
            (
                code: "School.InvalidSchoolType",
                description: "School type does not exist"
            );

            public static Error SchoolContainsStudents => Error.Conflict
            (
                code: "School.SchoolContainsStudents",
                description: "School contains students. Consider to replace or remove the student from school before deleting it."
            );
        }
    }
}