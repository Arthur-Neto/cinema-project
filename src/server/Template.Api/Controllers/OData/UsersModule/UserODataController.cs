﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Template.Application.UsersModule;
using Template.Application.UsersModule.Models;

namespace Template.Api.Controllers.Api.UsersModule
{
    [Route("odata/users")]
    public class UserODataController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserODataController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), 200)]
        public async Task<IActionResult> RetrieveAllAsync()
        {
            return Ok(await _userService.RetrieveAllAsync());
        }
    }
}
