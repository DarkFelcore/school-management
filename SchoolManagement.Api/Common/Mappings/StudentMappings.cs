using Mapster;
using SchoolManagement.Application.Students.Create;
using SchoolManagement.Application.Students.Update;
using SchoolManagement.Contracts.Students;
using SchoolManagement.Domain.Students;

namespace SchoolManagement.Api.Common.Mappings
{
    public class StudentMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Student, StudentResponse>()
                .Map(dest => dest.Id, src => src.StudentId.Value)
                .Map(dest => dest.SchoolName, src => src.School.Name)
                .Map(
                    dest => dest.LessonNames, 
                    src => src.StudentLessons
                        .Where(x => x.StudentId == src.StudentId)
                        .Select(s => s.Lesson.Subject)
                        .ToList()
                )
                .Map(dest => dest, src => src);

            config.NewConfig<CreateStudentRequest, CreateStudentCommand>()
                .Map(dest => dest, src => src);

            config.NewConfig<(UpdateStudentRequest Request, string Id), UpdateStudentCommand>()
                .Map(dest => dest.StudentId, src => src.Id)
                .Map(dest => dest, src => src.Request);

        }

    }
}