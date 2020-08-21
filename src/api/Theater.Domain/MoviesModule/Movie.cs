using System.Collections.Generic;
using Theater.Domain.MoviesModule.Enums;
using Theater.Domain.SessionsModule;

namespace Theater.Domain.MoviesModule
{
    public class Movie : Entity
    {
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public ScreenType ScreenType { get; set; }
        public AudioType AudioType { get; set; }

        // Reverse Navigation
        public virtual IEnumerable<Session> Sessions { get; set; }
    }
}
