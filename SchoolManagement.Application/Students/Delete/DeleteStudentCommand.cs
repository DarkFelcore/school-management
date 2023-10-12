using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Application.Students.Delete
{
    public record DeleteStudentCommand(string StudentId): IRequest<ErrorOr<Student>>;
}