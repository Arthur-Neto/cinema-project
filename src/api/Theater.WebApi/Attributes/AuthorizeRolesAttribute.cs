using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Theater.Domain.UsersModule.Enums;

namespace Theater.WebApi.Attributes
{
    public class AuthorizeRoles : AuthorizeAttribute
    {
        public AuthorizeRoles(params Role[] roles)
        {
            var allowedRolesAsStrings = roles.Select(x => Enum.GetName(typeof(Role), x));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
