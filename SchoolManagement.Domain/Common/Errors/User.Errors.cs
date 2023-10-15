using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace SchoolManagement.Domain.Common.Errors
{
    public partial class Errors
    {
        public static class User
        {
            public static Error DuplicateEmail => Error.Conflict(
                code: "ErrorCode.DuplicateEmail",
                description: "Email is already in use"
            );

            public static Error InvalidCredentials => Error.Validation(
                code: "ErrorCode.InvalidCredentials",
                description: "Invalid credentials"
            );
        }
    }
}