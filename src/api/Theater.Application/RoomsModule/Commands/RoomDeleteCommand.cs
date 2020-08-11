using AutoMapper;
using FluentValidation;
using Theater.Domain.RoomsModule;

namespace Theater.Application.RoomsModule.Commands
{
    public class RoomDeleteCommand
    {
        public int ID { get; set; }
    }

    public class RoomDeleteCommandMapping : Profile
    {
        public RoomDeleteCommandMapping()
        {
            CreateMap<Room, RoomDeleteCommand>()
                .ForMember(m => m.ID, opts => opts.MapFrom(src => src.ID))
                .ReverseMap();
        }
    }

    public class UserDeleteCommandValidator : AbstractValidator<RoomDeleteCommand>
    {
        public UserDeleteCommandValidator()
        {
            RuleFor(x => x.ID).NotEmpty().GreaterThan(0);
        }
    }
}
