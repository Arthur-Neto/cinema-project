using System;
using AutoMapper;
using FluentValidation;
using Theater.Domain.SessionsModule;

namespace Theater.Application.SessionsModule.Commands
{
    public class SessionCreateCommand
    {
        public DateTimeOffset Date { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
    }

    public class SessionCreateCommandMapping : Profile
    {
        public SessionCreateCommandMapping()
        {
            CreateMap<SessionCreateCommand, Session>()
                .ForMember(m => m.Date, opts => opts.MapFrom(src => src.Date.ToLocalTime()));
        }
    }

    public class SessionCreateCommandValidator : AbstractValidator<SessionCreateCommand>
    {
        public SessionCreateCommandValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.MovieId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.RoomId).NotEmpty().GreaterThan(0);
        }
    }
}
