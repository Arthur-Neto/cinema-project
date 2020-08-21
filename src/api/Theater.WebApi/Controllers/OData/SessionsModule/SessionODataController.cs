using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.SessionsModule;
using Theater.Application.SessionsModule.Models;

namespace Theater.WebApi.Controllers.OData.SessionsModule
{
    [ApiController]
    [Route("odata/sessions")]
    public class SessionODataController : ControllerBase
    {
        private readonly ISessionService _SessionService;

        public SessionODataController(ISessionService SessionService)
        {
            _SessionService = SessionService;
        }

        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<SessionModel>), 200)]
        public async Task<IActionResult> RetrieveAllAsync()
        {
            return Ok(await _SessionService.RetrieveAllAsync());
        }
    }
}
