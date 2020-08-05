using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.UsersModule;
using Theater.Application.UsersModule.Commands;
using Theater.Application.UsersModule.Models;
using Theater.Domain.UsersModule.Enums;
using Theater.WebApi.Attributes;

namespace Theater.WebApi.Controllers.Api.UsersModule
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(AuthenticatedUserModel), 200)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserAuthenticateCommand command)
        {
            var user = await _userService.AuthenticateAsync(command);

            return Ok(user);
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(UserModel), 200)]
        public async Task<IActionResult> RetrieveByIDAsync(int id)
        {
            return Ok(await _userService.RetrieveByIDAsync(id));
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateCommand command)
        {
            return Ok(await _userService.CreateAsync(command));
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult Update([FromBody] UserUpdateCommand command)
        {
            _userService.Update(command);

            return Ok(true);
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteAsync([FromBody] UserDeleteCommand command)
        {
            await _userService.DeleteAsync(command);

            return Ok(true);
        }
    }
}
