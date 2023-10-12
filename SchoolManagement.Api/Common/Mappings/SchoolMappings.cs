using Mapster;
using SchoolManagement.Application.Schools.Create;
using SchoolManagement.Application.Schools.Update;
using SchoolManagement.Contracts.Schools;
using SchoolManagement.Domain.Schools;

namespace SchoolManagement.Api.Common.Mappings
{
    public class SchoolMappings : IRegister
    {

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<School, SchoolResponse>()
                .Map(dest => dest.Id, src => src.SchoolId.Value)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.SchoolType, src => src.SchoolType.ToString())
                .Map(dest => dest.Students, src => src.Students.Select(x => x.StudentId.Value));

            config.NewConfig<CreateSchoolRequest, CreateSchoolCommand>()
                .Map(dest => dest, src => src);

            config.NewConfig<(UpdateSchoolRequest Request, string Id), UpdateSchoolCommand>()
                .Map(dest => dest.SchoolId, src => src.Id)
                .Map(dest => dest, src => src.Request);

        }
    }
}