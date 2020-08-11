using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.UsersModule;
using Theater.Application.UsersModule.Models;
using Theater.Domain.UsersModule.Enums;
using Theater.WebApi.Attributes;

namespace Theater.WebApi.Controllers.OData.UsersModule
{
    [ApiController]
    [Route("odata/users")]
    public class UserODataController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserODataController(IUserService userService)
        {
            _userService = userService;
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
        public async Task<IActionResult> RetrieveAllAsync()
        {
            return Ok(await _userService.RetrieveAllAsync());
        }
    }
}
