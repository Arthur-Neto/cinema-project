using System;
using System.Collections.Generic;
using AutoMapper;
using Theater.Domain.SessionsModule;

namespace Theater.Application.SessionsModule.Models
{
    public class SessionDashboardModel
    {
        public int ID { get; set; }
        public IEnumerable<DateTimeOffset> StartTimes { get; set; }
        public int RoomID { get; set; }
        public string RoomName { get; set; }
    }

    public class SessionDashboardModelMapping : Profile
    {
        public SessionDashboardModelMapping()
        {
            CreateMap<Session, SessionDashboardModel>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.StartTimes, opts => opts.Ignore())
                .ForMember(m => m.RoomID, opts => opts.MapFrom(src => src.Room.ID))
                .ForMember(m => m.RoomName, opts => opts.MapFrom(src => src.Room.Name))
                .ReverseMap();
        }
    }
}
