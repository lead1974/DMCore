using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMCore.Data.Core;
using DMCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace DMCore.Areas.Admin.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly GlobalService _service;
        private readonly IConfiguration _config;

        public IndexModel(IUnitOfWork unitOfWork, GlobalService service, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _service = service;
            _config = config;


        }

        [TempData]
        public string StatusMessage { get; set; }
        public void OnGet()
        {
        }
    }
}