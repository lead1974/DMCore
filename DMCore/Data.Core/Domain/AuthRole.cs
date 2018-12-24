using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DMCore.Data.Core.Domain
{
    public class AuthRole : IdentityRole
    {
        public string RoleName { get; set; }
    }
}
