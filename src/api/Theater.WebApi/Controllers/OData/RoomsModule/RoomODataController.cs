using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.RoomsModule;
using Theater.Application.RoomsModule.Models;

namespace Theater.WebApi.Controllers.OData.RoomsModule
{
    [Route("odata/rooms")]
    public class RoomODataController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomODataController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [AllowAnonymous]
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<RoomModel>), 200)]
        public async Task<IActionResult> RetrieveAllAsync()
        {
            return Ok(await _roomService.RetrieveAllAsync());
        }
    }
}
