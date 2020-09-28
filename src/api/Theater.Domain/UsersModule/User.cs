using System.Collections.Generic;
using Theater.Domain.SessionsModule;
using Theater.Domain.UsersModule.Enums;

namespace Theater.Domain.UsersModule
{
    public class User : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Role Role { get; set; }
        public string Token { get; set; }

        // Reverse Navigation
        public virtual IEnumerable<OccupiedChair> OccupiedChairs { get; set; }
    }
}
