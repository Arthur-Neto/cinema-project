using Theater.Domain.UsersModule;

namespace Theater.Domain.SessionsModule
{
    public class OccupiedChair : Entity
    {
        public int Number { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }

        // Reverse Navigation
        public virtual User User { get; set; }
        public virtual Session Session { get; set; }
    }
}
