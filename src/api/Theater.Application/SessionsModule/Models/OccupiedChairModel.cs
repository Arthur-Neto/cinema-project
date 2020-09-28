using AutoMapper;
using Theater.Domain.SessionsModule;

namespace Theater.Application.SessionsModule.Models
{
    public class OccupiedChairModel
    {
        public int Number { get; set; }
        public int SessionId { get; set; }
    }

    public class OccupiedChairModelMapping : Profile
    {
        public OccupiedChairModelMapping()
        {
            CreateMap<OccupiedChair, OccupiedChairModel>()
                .ForMember(m => m.Number, opts => opts.MapFrom(src => src.Number))
                .ForMember(m => m.SessionId, opts => opts.MapFrom(src => src.SessionId))
                .ReverseMap();
        }
    }
}
