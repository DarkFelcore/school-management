using Mapster;
using SchoolManagement.Application.Authentication.Common;
using SchoolManagement.Application.Authentication.Register;
using SchoolManagement.Contracts.Authentication;

namespace SchoolManagement.Api.Common.Mappings
{
    public class AuthenticationMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>()
                .Map(dest => dest, src => src);

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Id, src => src.User.UserId.Value)
                .Map(dest => dest.FirstName, src => src.User.FirstName)
                .Map(dest => dest.LastName, src => src.User.LastName)
                .Map(dest => dest.Email, src => src.User.Email)
                .Map(dest => dest.IsAdmin, src => src.User.IsAdmin)
                .Map(dest => dest, src => src);
        }
    }
}