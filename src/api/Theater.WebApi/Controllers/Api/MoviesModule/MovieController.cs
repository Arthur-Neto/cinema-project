using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.MoviesModule;
using Theater.Application.MoviesModule.Commands;
using Theater.Domain.UsersModule.Enums;
using Theater.WebApi.Attributes;

namespace Theater.WebApi.Controllers.Api.MoviesModule
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateAsync(MovieCreateCommand command)
        {
            return Ok(await _movieService.CreateAsync(command));
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpPost]
        [Route("update-cover")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateCoverAsync([FromForm] UpdateCoverCommand command)
        {
            return Ok(await _movieService.UpdateCoverAsync(command));
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateAsync(MovieUpdateCommand command)
        {
            return Ok(await _movieService.UpdateAsync(command));
        }

        [AuthorizeRoles(Role.Manager)]
        [Route("{id:int}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _movieService.DeleteAsync(id));
        }
    }
}
