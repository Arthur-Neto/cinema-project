using System;
using AutoMapper;
using Theater.Domain.SessionsModule;

namespace Theater.Application.SessionsModule.Models
{
    public class SessionDashboardModel
    {
        public int ID { get; set; }
        public DateTimeOffset Date { get; set; }
    }

    public class SessionDashboardModelMapping : Profile
    {
        public SessionDashboardModelMapping()
        {
            CreateMap<Session, SessionDashboardModel>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.Date, opts => opts.MapFrom(src => src.Date));
        }
    }
}
