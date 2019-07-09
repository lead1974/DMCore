using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using DNTBreadCrumb.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DMCore.Areas.Admin.Pages.DealCategories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<DealCategory> dcList { get; set; } = new List<DealCategory>();


        public void OnGet()
        {
            dcList = _unitOfWork.DealCategories.GetAll().ToList();
            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "Home",
                Url = "/index",
                Order = 1
            });

        }
        //public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        //{
        //    dcList = _unitOfWork.DealCategories.GetAll().ToList();
        //    return new JsonResult(dcList.ToDataSourceResult(request));
        //}
    }
}