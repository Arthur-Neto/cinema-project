﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Theater.Domain.MoviesModule;
using Theater.Domain.RoomsModule;
using Theater.Domain.SessionsModule;
using Theater.Domain.UsersModule;
using Theater.Domain.UsersModule.Enums;
using Theater.Infra.Data.EF.Context;

namespace Theater.WebApi.Extensions
{
    public static class SeedExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApiContext>();
                SeedData(context);
            }
        }

        private static void SeedData(ApiContext context)
        {
            var user = new User()
            {
                Username = "admin",
                Password = "123",
                Role = Role.Manager
            };

            var room = new Room()
            {
                Name = "Lorem ipsum dolor sit amet",
                NumberOfChairs = 50,
            };

            var room2 = new Room()
            {
                Name = "Lorem ipsum dolor sit amet 2",
                NumberOfChairs = 50,
            };

            var room3 = new Room()
            {
                Name = "Lorem ipsum dolor sit amet 3",
                NumberOfChairs = 50,
            };

            var movie = new Movie()
            {
                AudioType = Domain.MoviesModule.Enums.AudioType.Dubbed,
                Description = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.",
                Duration = "1:30",
                ImagePath = $"{Environment.CurrentDirectory}\\wwwroot\\movies-imgs\\1\\title.png",
                ScreenType = Domain.MoviesModule.Enums.ScreenType.Two_Dimension,
                Title = "At vero eos et accusamus et"
            };

            var session = new Session()
            {
                Date = new DateTimeOffset(2020, 08, 31, 13, 30, 00, new TimeSpan()),
                MovieId = 1,
                RoomId = 1,
            };

            var session2 = new Session()
            {
                Date = new DateTimeOffset(2020, 09, 01, 15, 30, 00, new TimeSpan()),
                MovieId = 1,
                RoomId = 1,
            };

            var session3 = new Session()
            {
                Date = new DateTimeOffset(2020, 09, 01, 13, 30, 00, new TimeSpan()),
                MovieId = 1,
                RoomId = 2,
            };

            var session4 = new Session()
            {
                Date = new DateTimeOffset(2020, 09, 01, 13, 30, 00, new TimeSpan()),
                MovieId = 1,
                RoomId = 3,
            };

            var session5 = new Session()
            {
                Date = new DateTimeOffset(2020, 09, 01, 15, 30, 00, new TimeSpan()),
                MovieId = 1,
                RoomId = 3,
            };

            var session6 = new Session()
            {
                Date = new DateTimeOffset(2020, 09, 02, 15, 30, 00, new TimeSpan()),
                MovieId = 1,
                RoomId = 1,
            };

            context.Add(user);

            context.Add(room);
            context.Add(room2);
            context.Add(room3);

            context.Add(movie);

            context.Add(session);
            context.Add(session2);
            context.Add(session3);
            context.Add(session4);
            context.Add(session5);
            context.Add(session6);

            context.SaveChanges();
        }
    }
}
