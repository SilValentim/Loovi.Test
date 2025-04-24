
using AutoMapper;
using Loovi.Test.Application.Auth.AuthenticateUser;
using Loovi.Test.Application.Tasks.CreateTask;
using Loovi.Test.Common.Auth.Models;
using Loovi.Test.WebApi.Features.Tasks.Common;

namespace Loovi.Test.WebApi.Features.Auth.AuthenticateUser
{
    public class AuthenticateUserProfile : Profile
    {
        public AuthenticateUserProfile()
        {
            CreateMap<AuthRequest, AuthenticateUserCommand>();
            CreateMap<AuthResult, AuthResponse>()
            .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.Token))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.User.Role))
            .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt));
        }
    }
}
