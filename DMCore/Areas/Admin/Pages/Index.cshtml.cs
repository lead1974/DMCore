using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCore.Areas.Admin.Pages
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
                Title = "Site Administration",
                Url = "/admin",
                Order = 1
            });

        }
    }
}
