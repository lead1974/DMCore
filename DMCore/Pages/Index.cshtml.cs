using System.Collections.Generic;
using System.Linq;
using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using DNTBreadCrumb.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCore.Pages
{
    [BreadCrumb(Title = "Home", UseDefaultRouteUrl = true, Order = 0)]
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
        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            dcList = _unitOfWork.DealCategories.GetAll().ToList();
            return new JsonResult(dcList.ToDataSourceResult(request));
        }
    }
}
