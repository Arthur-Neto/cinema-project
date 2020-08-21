using System;
using AutoMapper;
using Theater.Domain.SessionsModule;

namespace Theater.Application.SessionsModule.Models
{
    public class SessionModel
    {
        public int ID { get; set; }
        public DateTimeOffset Date { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
    }

    public class SessionModelMapping : Profile
    {
        public SessionModelMapping()
        {
            CreateMap<Session, SessionModel>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.Date, opts => opts.MapFrom(src => src.Date.ToLocalTime()))
                .ForMember(m => m.MovieId, opts => opts.MapFrom(src => src.MovieId))
                .ForMember(m => m.MovieTitle, opts => opts.MapFrom(src => src.Movie.Title))
                .ForMember(m => m.RoomId, opts => opts.MapFrom(src => src.RoomId))
                .ForMember(m => m.RoomName, opts => opts.MapFrom(src => src.Room.Name))
                .ReverseMap();
        }
    }
}
