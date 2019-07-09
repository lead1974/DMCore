using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMCore.Data.Core;
using DMCore.Services;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace DMCore.Areas.Admin.Pages.Blog
{
    public class CreateEditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly GlobalService _service;
        private readonly IConfiguration _config;
        private readonly HostingEnvironment _env;

        public CreateEditModel(IUnitOfWork unitOfWork, GlobalService service, IConfiguration config, HostingEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _config = config;
            _env = env;

        }

        public int postId = 0;
        public string pagetype = "";
        [TempData]
        public string StatusMessage { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnGet(int? id, string pagetype)
        {

            if (pagetype == null) this.pagetype = "edit";
            else this.pagetype = pagetype;

            postId = id ?? default(int);
            //if (postId > 0)
            //{
            //    post = _unitOfWork.Posts.SingleOrDefault(e => e.Id == postId);
            //}

            //if (post == null)
            //{
            //    post = new Data.Core.Domain.Blog.Post();
            //    post.FromDate = DateTime.Now;
            //    post.ToDate = DateTime.Now;

            //    post.CreatedBy = User.Identity.Name.Substring(13);
            //    post.CreatedTS = DateTime.Now;
            //}

            //categories = _unitOfWork.Categories.GetAll().ToList();
            //eventCategories = new List<EventCategory>();

            //foreach (var c in categories)
            //{
            //    eventCategories.Add(new EventCategory
            //    {
            //        Text = c.CategoryName,
            //        Value = Convert.ToString(c.Id)
            //    });
            //}


            //string breadcrumbURL = "";
            //if (!HttpContext.Request.Host.ToString().ToLower().Contains("localhost")) breadcrumbURL = _config.GetSection("SiteSettings")["IISSiteName"]; ;

            //this.AddBreadCrumb(new BreadCrumb
            //{
            //    Title = "Edit Event",
            //    Url = breadcrumbURL + HttpContext.Request.Path,
            //    Order = 2
            //});

            //if (id == null)
            //{
            //    return Page();
            //}

            //if (postId != 0)
            //{
            //    eventik = _unitOfWork.Events.GetByIdWithCategory(postId);

            //    if (eventik == null)
            //    {
            //        return RedirectToPage("./Error");
            //    }
            //}
            return Page();
        }

    }
}