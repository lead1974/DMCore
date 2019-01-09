﻿using System.Collections.Generic;
using System.Linq;
using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DMCore.Pages
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
        }
    }
}
