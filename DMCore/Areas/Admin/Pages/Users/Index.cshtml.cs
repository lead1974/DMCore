using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCore.Areas.Admin.Pages.Users
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "Manage Users",
                Url = "/admin/users",
                Order = 1
            });
        }
    }
}