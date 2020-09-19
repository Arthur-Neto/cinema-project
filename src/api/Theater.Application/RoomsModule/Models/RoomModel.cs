using AutoMapper;
using Theater.Domain.RoomsModule;

namespace Theater.Application.RoomsModule.Models
{
    public class RoomModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberOfChairs { get; set; }
    }

    public class RoomModelMapping : Profile
    {
        public RoomModelMapping()
        {
            CreateMap<RoomModel, Room>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(m => m.NumberOfChairs, opts => opts.MapFrom(src => src.NumberOfChairs))
                .ReverseMap();
        }
    }
}
