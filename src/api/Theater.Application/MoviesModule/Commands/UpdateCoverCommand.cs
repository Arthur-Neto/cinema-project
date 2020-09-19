using Microsoft.AspNetCore.Http;

namespace Theater.Application.MoviesModule.Commands
{
    public class UpdateCoverCommand
    {
        public int ID { get; set; }
        public IFormFile Image { get; set; }
    }
}
