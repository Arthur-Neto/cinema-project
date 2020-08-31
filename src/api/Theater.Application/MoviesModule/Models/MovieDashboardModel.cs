using System.Collections.Generic;
using AutoMapper;
using Theater.Application.SessionsModule.Models;
using Theater.Domain.MoviesModule;
using Theater.Domain.MoviesModule.Enums;

namespace Theater.Application.MoviesModule.Models
{
    public class MovieDashboardModel
    {
        public int ID { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public ScreenType ScreenType { get; set; }
        public AudioType AudioType { get; set; }
        public IList<SessionDashboardModel> Sessions { get; set; }
    }

    public class MovieDashboardModelMapping : Profile
    {
        public MovieDashboardModelMapping()
        {
            CreateMap<Movie, MovieDashboardModel>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.ImagePath, opts => opts.MapFrom(src => src.ImagePath))
                .ForMember(m => m.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(m => m.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(m => m.Duration, opts => opts.MapFrom(src => src.Duration))
                .ForMember(m => m.ScreenType, opts => opts.MapFrom(src => src.ScreenType))
                .ForMember(m => m.AudioType, opts => opts.MapFrom(src => src.AudioType))
                .ForMember(m => m.Sessions, opts => opts.Ignore())
                .ReverseMap();
        }
    }
}
