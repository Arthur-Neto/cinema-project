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
    [ApiController]
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
        public async Task<IActionResult> AuthenticateAsync(UserAuthenticateCommand command)
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

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateAsync(UserCreateCommand command)
        {
            return Ok(await _userService.CreateAsync(command));
        }

        [AuthorizeRoles(Role.Manager, Role.Client)]
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateAsync(UserUpdateCommand command)
        {
            return Ok(await _userService.UpdateAsync(command));
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteAsync(UserDeleteCommand command)
        {
            await _userService.DeleteAsync(command);

            return Ok(true);
        }
    }
}
