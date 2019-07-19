using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using DNTBreadCrumb.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DMCore.Areas.Admin.Pages.DealCategories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //public List<DealCategory> dcList { get; set; } = new List<DealCategory>();


        public void OnGet()
        {
            //dcList = _unitOfWork.DealCategories.GetAll().ToList();
            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "Manage Deal Categories",
                Url = "/Admin/DealCategories",
                Order = 2
            });

        }
    }
}