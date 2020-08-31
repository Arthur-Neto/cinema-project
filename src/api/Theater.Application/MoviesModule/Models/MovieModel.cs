using AutoMapper;
using Theater.Domain.MoviesModule;
using Theater.Domain.MoviesModule.Enums;

namespace Theater.Application.MoviesModule.Models
{
    public class MovieModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string ScreenName { get; set; }
        public string AudioName { get; set; }
    }

    public class MovieModelMapping : Profile
    {
        public MovieModelMapping()
        {
            CreateMap<Movie, MovieModel>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(m => m.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(m => m.Duration, opts => opts.MapFrom(src => src.Duration))
                .ForMember(m => m.ScreenName, opts => opts.MapFrom(src => src.AudioType == AudioType.Dubbed ? "Dubbed" : "Subtitled"))
                .ForMember(m => m.AudioName, opts => opts.MapFrom(src => src.ScreenType == ScreenType.Three_Dimension ? "3D" : "2D"))
                .ReverseMap();
        }
    }
}
