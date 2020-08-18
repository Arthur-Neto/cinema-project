using System;
using AutoMapper;
using Theater.Domain.MoviesModule;
using Theater.Domain.MoviesModule.Enums;

namespace Theater.Application.MoviesModule.Models
{
    public class MovieModel
    {
        public int ID { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duration { get; set; }
        public ScreenType ScreenType { get; set; }
        public AudioType AudioType { get; set; }
    }

    public class MovieModelMapping : Profile
    {
        public MovieModelMapping()
        {
            CreateMap<Movie, MovieModel>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.ImagePath, opts => opts.MapFrom(src => src.ImagePath))
                .ForMember(m => m.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(m => m.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(m => m.Duration, opts => opts.MapFrom(src => src.Duration))
                .ForMember(m => m.ScreenType, opts => opts.MapFrom(src => src.ScreenType))
                .ForMember(m => m.AudioType, opts => opts.MapFrom(src => src.AudioType))
                .ReverseMap();
        }
    }
}
