using System.Collections.Generic;
using AutoMapper;
using Theater.Application.RoomsModule.Models;
using Theater.Domain.MoviesModule;
using Theater.Domain.MoviesModule.Enums;
using Theater.Infra.Crosscutting.Extensions;

namespace Theater.Application.MoviesModule.Models
{
    public class MovieDashboardModel
    {
        public int ID { get; set; }
        public string ImageBase64 { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public ScreenType ScreenType { get; set; }
        public AudioType AudioType { get; set; }
        public IList<RoomDashboardModel> Rooms { get; set; }
    }

    public class MovieDashboardModelMapping : Profile
    {
        public MovieDashboardModelMapping()
        {
            CreateMap<Movie, MovieDashboardModel>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.ImageBase64, opts => opts.MapFrom(src => src.ImagePath.ConvertFilePathToBase64()))
                .ForMember(m => m.Title, opts => opts.MapFrom(src => src.Title))
                .ForMember(m => m.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(m => m.Duration, opts => opts.MapFrom(src => src.Duration.FormatDurationFromHourMinute()))
                .ForMember(m => m.ScreenType, opts => opts.MapFrom(src => src.ScreenType))
                .ForMember(m => m.AudioType, opts => opts.MapFrom(src => src.AudioType))
                .ForMember(m => m.Rooms, opts => opts.Ignore());
        }
    }
}
