using System;
using Theater.Domain.MoviesModule.Enums;

namespace Theater.Domain.MoviesModule
{
    public class Movie : Entity
    {
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Duration { get; set; }
        public ScreenType ScreenType { get; set; }
        public AudioType AudioType { get; set; }
    }
}
