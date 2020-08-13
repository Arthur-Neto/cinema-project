using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.RoomsModule;
using Theater.Application.RoomsModule.Commands;
using Theater.Domain.UsersModule.Enums;
using Theater.WebApi.Attributes;

namespace Theater.WebApi.Controllers.Api.RoomsModule
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateAsync(RoomCreateCommand command)
        {
            return Ok(await _roomService.CreateAsync(command));
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateAsync(RoomUpdateCommand command)
        {
            return Ok(await _roomService.UpdateAsync(command));
        }

        [AuthorizeRoles(Role.Manager)]
        [Route("{id:int}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _roomService.DeleteAsync(id));
        }
    }
}
