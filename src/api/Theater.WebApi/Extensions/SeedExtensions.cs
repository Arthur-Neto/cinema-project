using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Theater.Domain.RoomsModule;
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
                Name = "Sala 01",
                NumberOfChairs = 50,
            };
            var room2 = new Room()
            {
                Name = "Sala 02",
                NumberOfChairs = 50,
            };
            var room3 = new Room()
            {
                Name = "Sala 03",
                NumberOfChairs = 50,
            };
            var room4 = new Room()
            {
                Name = "Sala 04",
                NumberOfChairs = 50,
            };
            var room5 = new Room()
            {
                Name = "Sala 05",
                NumberOfChairs = 50,
            };

            context.Add(user);
            context.Add(room);
            context.Add(room2);
            context.Add(room3);
            context.Add(room4);
            context.Add(room5);

            context.SaveChanges();
        }
    }
}
