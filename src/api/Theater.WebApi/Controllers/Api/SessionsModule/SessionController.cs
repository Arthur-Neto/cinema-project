﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Theater.Application.SessionsModule;
using Theater.Application.SessionsModule.Commands;
using Theater.Domain.UsersModule.Enums;
using Theater.WebApi.Attributes;

namespace Theater.WebApi.Controllers.Api.SessionModules
{
    [ApiController]
    [Route("api/sessions")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _SessionService;

        public SessionController(ISessionService SessionService)
        {
            _SessionService = SessionService;
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> CreateAsync(SessionCreateCommand command)
        {
            return Ok(await _SessionService.CreateAsync(command));
        }

        [AuthorizeRoles(Role.Manager)]
        [HttpPut]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> UpdateAsync(SessionUpdateCommand command)
        {
            return Ok(await _SessionService.UpdateAsync(command));
        }

        [AuthorizeRoles(Role.Manager)]
        [Route("{id:int}")]
        [HttpDelete]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return Ok(await _SessionService.DeleteAsync(id));
        }
    }
}
