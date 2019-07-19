using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMCore.Data.Core;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace DMCore.Pages.Deals
{
    public class PostModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public PostModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public Data.Core.Domain.Deal.Deal deal { get; set; }
        public IActionResult OnGet()
        {
            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "Post New Deal",
                Url = "/Deal/Post",
                Order = 1
            });

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.Deals.Add(deal);
            _unitOfWork.Complete();

            return RedirectToPage("/Default");

        }
    }
}