using DMCore.Data.Core;
using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Repositories;
using DMCore.Data.Persistance;
using DMCore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data
{
    public class DMSeedData
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly RoleManager<AuthRole> _roleManager;
        private readonly ILogger _logger;
        private GlobalService _globalService;

        public DMSeedData(
            IUnitOfWork unitOfWork,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            RoleManager<AuthRole> roleManager,
            GlobalService globalService,
            ILoggerFactory loggerFactory)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _globalService = globalService;
            _logger = loggerFactory.CreateLogger<DMSeedData>();

        }

        public async Task EnsureSeedData()
        {
            SeedAdminUsers();
            await SeedDealCategories();
            //await SeedDeals();
            //await SeedCoupons();
            //await SeedAffiliateSites();
            //await SeedStores();
        }

        private async void SeedAdminUsers()
        {
            var user = new AuthUser
            {
                UserName = "balda@balda.com",
                NormalizedUserName = "balda@balda.com",
                Email = "balda@balda.com",
                NormalizedEmail = "balda@balda.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<AuthRole>(_unitOfWork._context);

            if (!_unitOfWork._context.Roles.Any(r => r.Name == SD.CanManageSite))
            {
                await roleStore.CreateAsync(new AuthRole { Name = SD.CanManageSite, NormalizedName = SD.CanManageSite, RoleName = "Site Administrator" });
            }

            if (!_unitOfWork._context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<AuthUser>();
                var hashed = password.HashPassword(user, "balda1234");
                user.FirstName = "";
                user.LastName = "";
                user.PhoneNumber = "";
                user.PasswordHash = hashed;
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, SD.CanManageSite);
                }
            }
                _unitOfWork.Complete();
        }

        private async Task SeedDealCategories()
        {
            int dealCategoriesCount = await _unitOfWork.DealCategories.GetCount();

            if (dealCategoriesCount == 0)
            {
                var dCategories = new DealCategory[] {
                new DealCategory
                {
                     
                    Name = "Autos",
                    ShortName = "Autos",
                    Status = c.Status.Active.ToString(),
                    SortSeq = 1,
                    PublicCategory = true,
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Entertainment",
                    ShortName = "Entertainment",
                    Status = c.Status.Active.ToString(),
                    SortSeq = 2,
                    PublicCategory = true,
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Books & Magazines",
                    ShortName = "Books",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Children",
                    ShortName = "Children",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Clothing & Accessories",
                    ShortName = "Apparel",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Computers & Accessories",
                    ShortName = "Computers",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Education",
                    ShortName = "Education",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Credit Cards",
                    ShortName = "Finance",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Flowers & Gifts",
                    ShortName = "Flowers",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Freebies",
                    ShortName = "Freebies",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Grocery",
                    ShortName = "Grocery",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },

                new DealCategory
                {

                    Name = "Health & Beauty",
                    ShortName = "Health",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Home & Home Improvement",
                    ShortName = "Home",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Jewerly & Watches",
                    ShortName = "Jewerly",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },

                new DealCategory
                {

                    Name = "Office & School Supplies",
                    ShortName = "Office",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Pets",
                    ShortName = "Pets",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Phones & Accessories",
                    ShortName = "Phones",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Restaurants",
                    ShortName = "Restaurants",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Services",
                    ShortName = "Services",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {
                    Name = "Shoes",
                    ShortName = "Shoes",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Sports & Outdoors",
                    ShortName = "Sports",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Tech & Electronics",
                    ShortName = "Electronics",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Toys",
                    ShortName = "Toys",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Travel and Vacations",
                    ShortName = "Travel",
                    SortSeq = 2,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "Other",
                    ShortName = "Other",
                    SortSeq = 999,
                    PublicCategory = true,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
                new DealCategory
                {

                    Name = "General Store",
                    ShortName = "General",
                    SortSeq = 888,
                    PublicCategory = false,
                    Status = c.Status.Active.ToString(),
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
            };

                _unitOfWork.DealCategories.AddRange(dCategories);
                _unitOfWork.Complete();
            }
        }

    }
}
