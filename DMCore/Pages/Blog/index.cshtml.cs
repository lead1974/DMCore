using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCore.Pages.Blog
{
    public class indexModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Blog";

            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "Blog",
                Url = "/blog",
                Order = 2
            });

        }
    }
}