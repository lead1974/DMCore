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
            await SeedAdminUsers();
            await SeedDealCategories();
            //await SeedStores();
            //await SeedDeals();
            //await SeedCoupons();
            //await SeedAffiliateSites();

        }

        private async Task SeedAdminUsers()
        {
            var user = new AuthUser
            {
                UserName = "email2lead@gmail.com",
                NormalizedUserName = "email2lead@gmail.comm",
                Email = "email2lead@gmail.com",
                NormalizedEmail = "email2lead@gmail.com",
                FirstName = "",
                LastName = "",
                PhoneNumber = "",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //Seed Roles
            //var roleStore = new RoleStore<AuthRole>(_unitOfWork._context);

            //if (!_unitOfWork._context.Roles.Any(r => r.Name == SD.CanManageSite))
            //{
            //    await roleStore.CreateAsync(new AuthRole { Name = SD.CanManageSite, NormalizedName = SD.CanManageSite, RoleName = "Site Administrator" });
            //}
            if (await _roleManager.FindByNameAsync(SD.CanManageSite) == null)
            {
                await _roleManager.CreateAsync(new AuthRole { Name = SD.CanManageSite, NormalizedName = SD.CanManageSite, RoleName = "Site Administrator" });
            }

            //Seed Users
            if (await _userManager.FindByEmailAsync(user.Email) == null)
            {
                //var password = new PasswordHasher<AuthUser>();
                //var hashed = password.HashPassword(user, "balda4321");
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    var password = "balda1234";
                    await _userManager.AddPasswordAsync(user, password);
                    await _userManager.AddToRoleAsync(user, SD.CanManageSite);
                }
            }

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
                    FAIcon="fas fa-car",
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
                    FAIcon="fas fa-theater-masks",
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
                    FAIcon="fas fa-book-reader",
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
                    FAIcon="fas fa-child",
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
                    FAIcon="fas fa-luggage-cart",
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
                    FAIcon ="fas fa-laptop-code",
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
                    FAIcon="fas fa-user-graduate",
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
                    FAIcon="fas fa-piggy-bank",
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
                    FAIcon="fas fa-gift",
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
                    FAIcon="fas fa-hand-holding-usd",
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
                    FAIcon="fas fa-shopping-cart",
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
                    FAIcon="fas fa-medkit",
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
                    FAIcon="fas fa-home",
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
                    FAIcon="far fa-clock",
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
                    FAIcon="far fa-clipboard",
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
                    FAIcon="fas fa-paw",
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
                    FAIcon="fas fa-phone",
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
                    FAIcon="fas fa-wine-bottle",
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
                    FAIcon="fas fa-shipping-fast",
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
                    FAIcon="fas fa-shoe-prints",
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
                    FAIcon="fas fa-football-ball",
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
                    FAIcon="fab fa-android",
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
                    FAIcon="fas fa-dove",
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
                    FAIcon="fas fa-umbrella-beach",
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
                    FAIcon="fas fa-yin-yang",
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
                    FAIcon="fas fa-shopping-cart",
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

        private async Task SeedStores()
        {
            int storesCount = await _unitOfWork.Stores.GetCount();

            if (storesCount == 0)
            {
                var stores = new Store[] {
                new Store
                {

                    Name = "Amazon",
                    Domain="amazon.com",
                    Instructions="",
                    StoreTips="",
                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
            };

                _unitOfWork.Stores.AddRange(stores);
                _unitOfWork.Complete();
            }
        }
        private async Task SeedDeals()
        {
            int dealsCount = await _unitOfWork.Deals.GetCount();

            if (dealsCount == 0)
            {
                var deals = new Deal[] {
                new Deal
                {

                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
            };

                _unitOfWork.Deals.AddRange(deals);
                _unitOfWork.Complete();
            }
        }

        private async Task SeedCoupons()
        {
            int couponsCount = await _unitOfWork.Coupons.GetCount();

            if (couponsCount == 0)
            {
                var coupons = new Coupon[] {
                new Coupon
                {

                    UpdatedTS= DateTime.UtcNow,
                    UpdatedBy="System",
                    CreatedTS= DateTime.UtcNow,
                    CreatedBy="System"
                },
            };

                _unitOfWork.Coupons.AddRange(coupons);
                _unitOfWork.Complete();
            }
        }

    }
}
