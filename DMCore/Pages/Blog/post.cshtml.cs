using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCore.Pages.Blog
{
    public class postModel : PageModel
    {
        public void OnGet()
        {
            ViewData["Title"] = "Blog Post";

            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "Blog",
                Url = "/Blog/Index",
                Order = 2
            });
            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "Post Name Here",
                Url = "/Blog/Post",
                Order = 3
            });
        }
    }
}