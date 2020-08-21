using System.Collections.Generic;
using Theater.Domain.SessionsModule;

namespace Theater.Domain.RoomsModule
{
    public class Room : Entity
    {
        public string Name { get; set; }
        public int NumberOfChairs { get; set; }

        // Reverse Navigation
        public virtual IEnumerable<Session> Sessions { get; set; }
    }
}
