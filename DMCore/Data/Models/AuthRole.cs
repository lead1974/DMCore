using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Models
{
    public class AuthRole : IdentityRole
    {
        public string RoleName { get; set; }
    }
}
