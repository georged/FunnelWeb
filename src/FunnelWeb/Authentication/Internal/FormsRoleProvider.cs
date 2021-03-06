﻿using System;

namespace FunnelWeb.Authentication.Internal
{
    public class FormsRoleProvider : IRoleProvider
    {
        public bool IsUserInRole(string username, string roleName)
        {
            return true;
        }

        public string[] GetRolesForUser(string username)
        {
            return new[] {"Admin", "Moderator"};
        }
    }
}