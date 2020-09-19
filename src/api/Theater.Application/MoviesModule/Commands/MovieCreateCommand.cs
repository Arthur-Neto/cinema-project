using AutoMapper;
using FluentValidation;
using Theater.Domain.MoviesModule;
using Theater.Domain.MoviesModule.Enums;
using Theater.Infra.Crosscutting.Extensions;

namespace Theater.Application.MoviesModule.Commands
{
    public class MovieCreateCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public ScreenType ScreenType { get; set; }
        public AudioType AudioType { get; set; }
    }

    public class MovieCreateCommandMapping : Profile
    {
        public MovieCreateCommandMapping()
        {
            CreateMap<MovieCreateCommand, Movie>()
                .ForMember(m => m.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(m => m.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(m => m.Duration, opts => opts.MapFrom(src => src.Duration.FormatDurationFromMinutes()))
                .ForMember(m => m.ScreenType, opts => opts.MapFrom(src => src.ScreenType))
                .ForMember(m => m.AudioType, opts => opts.MapFrom(src => src.AudioType));
        }
    }

    public class MovieCreateCommandValidator : AbstractValidator<MovieCreateCommand>
    {
        public MovieCreateCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(1, 50);
            RuleFor(x => x.Description).NotEmpty().Length(1, 200);
            RuleFor(x => x.Duration).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ScreenType).NotEmpty().IsInEnum();
            RuleFor(x => x.AudioType).NotEmpty().IsInEnum();
        }
    }
}
