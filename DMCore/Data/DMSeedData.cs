using DMCore.Data.Models;
using DMCore.Data.Repositories;
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
        private readonly UserManager<AuthUser> _userManager;
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly RoleManager<AuthRole> _roleManager;
        private DMDbContext _context;
        private readonly ILogger _logger;

        private IDealRepository _dealRepo;
        private GlobalService _globalService;
        public DMSeedData(
            DMDbContext dbContext,
            IDealRepository dealRepo,
            UserManager<AuthUser> userManager,
            SignInManager<AuthUser> signInManager,
            RoleManager<AuthRole> roleManager,
            GlobalService globalService,
            ILoggerFactory loggerFactory)
        {
            _context = dbContext;
            _dealRepo = dealRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _globalService = globalService;
            _logger = loggerFactory.CreateLogger<DMSeedData>();

        }

        public async Task EnsureSeedData()
        {
            await SeedAdminUsers();
            //await SeedDealCategories();
            //await SeedDeals();
            //await SeedCouponCategories();
            //await SeedCoupons();
            //await SeedAffiliateSites();
            //await SeedStoreCategories();
            //await SeedStores();
        }

        private async Task SeedAdminUsers()
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

            var roleStore = new RoleStore<AuthRole>(_context);

            if (!_context.Roles.Any(r => r.Name == RoleName.CanManageSite))
            {
                await roleStore.CreateAsync(new AuthRole { Name = RoleName.CanManageSite, NormalizedName = RoleName.CanManageSite, RoleName = "Site Administrator" });
            }

            if (!_context.Roles.Any(r => r.Name == RoleName.CanManageInvoices))
            {
                await roleStore.CreateAsync(new AuthRole { Name = RoleName.CanManageInvoices, NormalizedName = RoleName.CanManageInvoices, RoleName = "Can Manage Invoices" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<AuthUser>();
                var hashed = password.HashPassword(user, "balda1234");
                user.PasswordHash = hashed;
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, RoleName.CanManageSite);
            }

            await _context.SaveChangesAsync();
        }
    }
}
