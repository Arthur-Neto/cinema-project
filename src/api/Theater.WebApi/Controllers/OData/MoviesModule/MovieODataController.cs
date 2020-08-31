using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.MoviesModule;
using Theater.Application.MoviesModule.Models;
using Theater.Domain.UsersModule.Enums;
using Theater.WebApi.Attributes;

namespace Theater.WebApi.Controllers.OData.MoviesModule
{
    [ApiController]
    [Route("odata/movies")]
    public class MovieODataController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieODataController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<MovieModel>), 200)]
        public async Task<IActionResult> RetrieveAllAsync()
        {
            return Ok(await _movieService.RetrieveAllAsync());
        }
    }
}
