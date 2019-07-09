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
    public class KendoSample : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<DealCategory> DcList { get; set; } = new List<DealCategory>();

        public KendoSample(IUnitOfWork unitOfWork)
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
        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, DealCategory dCategory)
        {
            if (dCategory.Name != null && ModelState.IsValid)
            {
                _unitOfWork.DealCategories.Add(dCategory);
                _unitOfWork.Complete();
            }
            else { }

            return new JsonResult(new[] { dCategory }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, DealCategory dCategory)
        {
            _unitOfWork.DealCategories.Update(dCategory);
            _unitOfWork.Complete();

            return new JsonResult(new[] { dCategory }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, DealCategory dCategory)
        {
            _unitOfWork.DealCategories.Remove(dCategory);
            _unitOfWork.Complete();

            return new JsonResult(new[] { dCategory }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult OnPostSave(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}