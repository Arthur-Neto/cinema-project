using AutoMapper;
using FluentValidation;
using Theater.Domain.MoviesModule;
using Theater.Domain.MoviesModule.Enums;

namespace Theater.Application.MoviesModule.Commands
{
    public class MovieCreateCommand
    {
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public ScreenType ScreenType { get; set; }
        public AudioType AudioType { get; set; }
    }

    public class MovieCreateCommandMapping : Profile
    {
        public MovieCreateCommandMapping()
        {
            CreateMap<Movie, MovieCreateCommand>()
                .ForMember(m => m.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(m => m.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(m => m.Duration, opts => opts.MapFrom(src => src.Duration))
                .ForMember(m => m.ScreenType, opts => opts.MapFrom(src => src.ScreenType))
                .ForMember(m => m.AudioType, opts => opts.MapFrom(src => src.AudioType))
                .ReverseMap();
        }
    }

    public class MovieCreateCommandValidator : AbstractValidator<MovieCreateCommand>
    {
        public MovieCreateCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(1, 50);
            RuleFor(x => x.Description).NotEmpty().Length(1, 50);
            RuleFor(x => x.Duration).NotEmpty().Length(1, 50);
            RuleFor(x => x.ScreenType).NotEmpty().IsInEnum();
            RuleFor(x => x.AudioType).NotEmpty().IsInEnum();
        }
    }
}
