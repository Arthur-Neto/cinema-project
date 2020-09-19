using AutoMapper;
using FluentValidation;
using Theater.Domain.RoomsModule;

namespace Theater.Application.RoomsModule.Commands
{
    public class RoomUpdateCommand
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberOfChairs { get; set; }
    }

    public class RoomUpdateCommandMapping : Profile
    {
        public RoomUpdateCommandMapping()
        {
            CreateMap<RoomUpdateCommand, Room>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ForMember(m => m.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(m => m.NumberOfChairs, opts => opts.MapFrom(src => src.NumberOfChairs));
        }
    }

    public class RoomUpdateCommandValidator : AbstractValidator<RoomUpdateCommand>
    {
        public RoomUpdateCommandValidator()
        {
            RuleFor(x => x.ID).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().Length(1, 50);
            RuleFor(x => x.NumberOfChairs).NotEmpty().InclusiveBetween(20, 100);
        }
    }
}
