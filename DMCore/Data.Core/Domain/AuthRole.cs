using Microsoft.AspNetCore.Identity;

namespace DMCore.Data.Core.Domain
{
    public class AuthRole : IdentityRole
    {
        public string RoleName { get; set; }
    }
}
