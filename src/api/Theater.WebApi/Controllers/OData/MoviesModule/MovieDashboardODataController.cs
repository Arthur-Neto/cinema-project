using System;
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
    [Route("odata/movies-dashboard")]
    public class MovieDashboardODataController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieDashboardODataController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<MovieDashboardModel>), 200)]
        public async Task<IActionResult> RetrieveMoviesDashboardAsync(DateTime date)
        {
            return Ok(await _movieService.RetrieveMoviesDashboardAsync(date));
        }
    }
}
