using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.MoviesModule;
using Theater.Application.MoviesModule.Models;

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

        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<MovieModel>), 200)]
        public async Task<IActionResult> RetrieveAllAsync()
        {
            return Ok(await _movieService.RetrieveAllAsync());
        }
    }
}
