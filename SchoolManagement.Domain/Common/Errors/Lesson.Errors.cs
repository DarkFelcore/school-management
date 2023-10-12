using ErrorOr;

namespace SchoolManagement.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Lesson
        {
            public static Error NotFound => Error.NotFound
            (
                code: "Lesson.NotFound",
                description: "Lesson not found"
            );

            public static Error DuplicateSubject => Error.Validation
            (
                code: "Lesson.DuplicateSubject",
                description: "Duplicate subject name"
            );

            public static Error InvalidSubject => Error.NotFound
            (
                code: "Lesson.InvalidSubject",
                description: "Invalid subject name"
            );
        }
    }
}