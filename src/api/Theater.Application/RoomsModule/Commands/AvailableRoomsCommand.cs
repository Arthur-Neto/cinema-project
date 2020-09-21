using System;
using FluentValidation;

namespace Theater.Application.RoomsModule.Commands
{
    public class AvailableRoomsCommand
    {
        public DateTimeOffset Date { get; set; }
        public string MovieDuration { get; set; }
    }

    public class AvailableRoomsCommandValidator : AbstractValidator<AvailableRoomsCommand>
    {
        public AvailableRoomsCommandValidator()
        {
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.MovieDuration).NotEmpty();
        }
    }
}
