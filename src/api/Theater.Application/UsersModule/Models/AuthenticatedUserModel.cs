using AutoMapper;
using Theater.Domain.UsersModule;
using Theater.Domain.UsersModule.Enums;

namespace Theater.Application.UsersModule.Models
{
    public class AuthenticatedUserModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }

    public class AuthenticatedUserModelMapping : Profile
    {
        public AuthenticatedUserModelMapping()
        {
            CreateMap<User, AuthenticatedUserModel>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.Username, opts => opts.MapFrom(src => src.Username))
                .ForMember(m => m.Role, opts => opts.MapFrom(src => src.Role))
                .ForMember(m => m.Token, opts => opts.MapFrom(src => src.Token))
                .ReverseMap();
        }
    }
}
