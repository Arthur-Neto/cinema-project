using System;
using AutoMapper;
using FluentValidation;
using Theater.Domain.SessionsModule;

namespace Theater.Application.SessionsModule.Commands
{
    public class SessionCreateCommand
    {
        public DateTime Date { get; set; }
    }

    public class SessionCreateCommandMapping : Profile
    {
        public SessionCreateCommandMapping()
        {
            CreateMap<Session, SessionCreateCommand>()
                .ForMember(m => m.Date, opts => opts.MapFrom(src => src.Date))
                .ReverseMap();
        }
    }

    public class SessionCreateCommandValidator : AbstractValidator<SessionCreateCommand>
    {
        public SessionCreateCommandValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
