using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using SchoolManagement.Application.Common.Interfaces;
using SchoolManagement.Domain.Common.Errors;
using SchoolManagement.Domain.Lessons;

namespace SchoolManagement.Application.Lessons.Get
{
    public class GetAllLessonsQueryHandler : IRequestHandler<GetAllLessonsQuery, ErrorOr<List<Lesson>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllLessonsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<List<Lesson>>> Handle(GetAllLessonsQuery query, CancellationToken cancellationToken)
        {
            var lessons = await _unitOfWork.LessonRepository.AllAsync();

            if(lessons is null)
            {
                return Errors.Lesson.NotFound;
            }

            return lessons;
        }
    }
}