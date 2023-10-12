using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using SchoolManagement.Application.Lessons.Create;
using SchoolManagement.Contracts.Lessons;
using SchoolManagement.Domain.Lessons;

namespace SchoolManagement.Api.Common.Mappings
{
    public class LessonMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Lesson, LessonResponse>()
                .Map(dest => dest.Id, src => src.LessonId.Value)
                .Map(
                    dest => dest.Students, 
                    src => src.StudentLessons
                        .Where(x => x.LessonId == src.LessonId)
                        .Select(x => x.StudentId.Value)
                        .ToList() 
                )
                .Map(dest => dest, src => src);
        
            config.NewConfig<CreateLessonRequest, CreateLessonCommand>()
                .Map(dest => dest, src => src);
        }
    }
}