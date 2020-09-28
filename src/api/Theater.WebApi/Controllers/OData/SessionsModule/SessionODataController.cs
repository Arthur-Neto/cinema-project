using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.SessionsModule;
using Theater.Application.SessionsModule.Models;
using Theater.Domain.UsersModule.Enums;
using Theater.WebApi.Attributes;

namespace Theater.WebApi.Controllers.OData.SessionsModule
{
    [ApiController]
    [Route("odata/sessions")]
    public class SessionODataController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionODataController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<SessionModel>), 200)]
        public async Task<IActionResult> RetrieveAllAsync()
        {
            return Ok(await _sessionService.RetrieveAllAsync());
        }

        [AuthorizeRoles(Role.Manager, Role.Client)]
        [HttpGet]
        [Route("occupied-chairs")]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<OccupiedChairModel>), 200)]
        public async Task<IActionResult> GetOccupiedChairsAsync()
        {
            return Ok(await _sessionService.GetOccupiedChairsAsync());
        }
    }
}
