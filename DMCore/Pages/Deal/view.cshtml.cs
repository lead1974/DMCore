using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCoreV1.Pages.Deals
{
    public class viewModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "View Deal";
            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "View Deal",
                Url = "/Deals/View",
                Order = 2
            });
        }
    }
}