using System.Threading.Tasks;
using DMCore.Data.Core.Domain;
using DNTBreadCrumb.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DMCore.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<AuthUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "My Account",
                Url = "/Identity/Account/Manage",
                Order = 1
            });
            this.AddBreadCrumb(new BreadCrumb
            {
                Title = "My Personal Data",
                Url = "/Identity/Account/Manage/PersonalData",
                Order = 2
            });

            return Page();
        }
    }
}