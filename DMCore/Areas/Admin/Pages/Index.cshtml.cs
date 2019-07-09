using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMCore.Data;
using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCore.Areas.Admin.Pages
{
    [Authorize(Policy = SD.PolicyCanManageSite)]
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
                Title = "Site Administration",
                Url = "/admin",
                Order = 1
            });

        }
    }
}
