using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.ReportsModule;
using Theater.Application.ReportsModule.Models;
using Theater.Domain.UsersModule.Enums;
using Theater.WebApi.Attributes;

namespace Theater.WebApi.Controllers.OData.ReportsModule
{
    [ApiController]
    [Route("odata/reports")]
    public class ReportODataController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportODataController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpGet]
        [Route("movies-billing")]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<MoviesBillingModel>), 200)]
        public async Task<IActionResult> GetMoviesBillingAsync()
        {
            return Ok(await _reportService.GetMoviesBillingAsync());
        }
    }
}
