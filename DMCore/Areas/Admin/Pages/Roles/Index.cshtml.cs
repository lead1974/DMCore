using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCore.Areas.Admin.Pages.Roles
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "Manage Roles",
                Url = "/admin/roles",
                Order = 1
            });
        }
    }
}