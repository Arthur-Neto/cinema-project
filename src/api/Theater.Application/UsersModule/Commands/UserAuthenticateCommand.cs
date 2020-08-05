using AutoMapper;
using FluentValidation;
using Theater.Domain.UsersModule;

namespace Theater.Application.UsersModule.Commands
{
    public class UserAuthenticateCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserAuthenticateCommandMapping : Profile
    {
        public UserAuthenticateCommandMapping()
        {
            CreateMap<User, UserAuthenticateCommand>()
                .ForMember(m => m.Username, opts => opts.MapFrom(src => src.Username))
                .ForMember(m => m.Password, opts => opts.MapFrom(src => src.Password))
                .ReverseMap();
        }
    }

    public class UserAuthenticateCommandValidator : AbstractValidator<UserAuthenticateCommand>
    {
        public UserAuthenticateCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().Length(1, 50);
            RuleFor(x => x.Password).NotEmpty().Length(1, 50);
        }
    }
}
