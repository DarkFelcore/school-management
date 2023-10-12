using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Application.Schools.Get
{
    public record GetSchoolQuery(string StudentId): IRequest<ErrorOr<School>>;
}