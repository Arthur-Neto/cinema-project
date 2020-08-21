using System;
using Theater.Domain.MoviesModule;
using Theater.Domain.RoomsModule;

namespace Theater.Domain.SessionsModule
{
    public class Session : Entity
    {
        public DateTimeOffset Date { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }

        // Reverse Navigation
        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }
    }
}
