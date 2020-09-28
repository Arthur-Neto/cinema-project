using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace Theater.Application.SessionsModule.Commands
{
    public class OccupiedChairsCommand
    {
        public IEnumerable<int> ChairsNumbers { get; set; }
        public int SessionId { get; set; }
    }

    public class OccupiedChairsCommandValidator : AbstractValidator<OccupiedChairsCommand>
    {
        public OccupiedChairsCommandValidator()
        {
            RuleFor(x => x.SessionId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.ChairsNumbers).NotEmpty().Must(p => p.Count() > 0);
        }
    }
}
