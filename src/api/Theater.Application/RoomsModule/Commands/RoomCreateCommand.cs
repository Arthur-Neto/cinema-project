using AutoMapper;
using FluentValidation;
using Theater.Domain.RoomsModule;

namespace Theater.Application.RoomsModule.Commands
{
    public class RoomCreateCommand
    {
        public string Name { get; set; }
        public int NumberOfChairs { get; set; }
    }

    public class RoomCreateCommandMapping : Profile
    {
        public RoomCreateCommandMapping()
        {
            CreateMap<Room, RoomCreateCommand>()
                .ForMember(m => m.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(m => m.NumberOfChairs, opts => opts.MapFrom(src => src.NumberOfChairs))
                .ReverseMap();
        }
    }

    public class RoomCreateCommandValidator : AbstractValidator<RoomCreateCommand>
    {
        public RoomCreateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 50);
            RuleFor(x => x.NumberOfChairs).NotEmpty().InclusiveBetween(20, 100);
        }
    }
}
