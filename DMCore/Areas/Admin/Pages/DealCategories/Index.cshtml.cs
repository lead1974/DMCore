using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using DNTBreadCrumb.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace DMCore.Areas.Admin.Pages.DealCategories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<DealCategory> DcList { get; set; } = new List<DealCategory>();

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            DcList = _unitOfWork.DealCategories.GetAll().ToList();

            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "Manage Deal Categories",
                Url = "/admin/dealcategories",
                Order = 1
            });
        }
        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            DcList = _unitOfWork.DealCategories.GetAll().ToList();
            return new JsonResult(DcList.ToDataSourceResult(request));
        }
        public JsonResult OnPostReadRecords()
        {
            List<DealCategory> dCatList = _unitOfWork.DealCategories.GetAll().ToList();

            return new JsonResult(dCatList);
        }

        public JsonResult OnPostUpdateRecord([DataSourceRequest] DataSourceRequest request, DealCategory dCategory)
        {
            System.Diagnostics.Debug.WriteLine("Updating");

            return new JsonResult(dCategory);
        }
    }
}