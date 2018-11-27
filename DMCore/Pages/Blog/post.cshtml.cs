using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCore.Pages.Blog
{
    public class postModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Blog Post";
        }
    }
}