using System.Collections.Generic;
using AutoMapper;
using Theater.Application.SessionsModule.Models;
using Theater.Domain.RoomsModule;

namespace Theater.Application.RoomsModule.Models
{
    public class RoomDashboardModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberOfChairs { get; set; }
        public IEnumerable<SessionDashboardModel> Sessions { get; set; }
    }

    public class RoomDashboardModelMapping : Profile
    {
        public RoomDashboardModelMapping()
        {
            CreateMap<Room, RoomDashboardModel>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(m => m.NumberOfChairs, opts => opts.MapFrom(src => src.NumberOfChairs))
                .ForMember(m => m.Sessions, opts => opts.Ignore());
        }
    }
}
