using System;
using AutoMapper;
using FluentValidation;
using Theater.Domain.SessionsModule;

namespace Theater.Application.SessionsModule.Commands
{
    public class SessionUpdateCommand
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
    }

    public class SessionUpdateCommandMapping : Profile
    {
        public SessionUpdateCommandMapping()
        {
            CreateMap<SessionUpdateCommand, Session>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.Date, opts => opts.MapFrom(src => src.Date));
        }
    }

    public class SessionUpdateCommandValidator : AbstractValidator<SessionUpdateCommand>
    {
        public SessionUpdateCommandValidator()
        {
            RuleFor(x => x.ID).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Date).NotEmpty();
        }
    }
}
