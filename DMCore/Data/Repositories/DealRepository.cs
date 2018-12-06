using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DMCore.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DMCore.Data.Repositories
{
    public class DealRepository : IDealRepository
    {
        private readonly DMDbContext _context;
        private readonly UserManager<AuthUser> _userManager;
        private readonly ILogger<DealRepository> _logger;

        public DealRepository(DMDbContext context, 
                              UserManager<AuthUser> userManager,
                              ILogger<DealRepository> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IEnumerable<Deal>> GetAll()
        {
           try
            {
                return await _context.Deals.OrderByDescending(t => t.CreatedTS).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get deals from database", ex);
                return null;
            }
         }
        public int GetCount()
        {
            try
            {
                return _context.Deals.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get deals from database", ex);
                return 0;
            }
        }


        public async Task<Deal> GetById(long Id)
        {
            return await _context.Deals.SingleOrDefaultAsync(d => d.Id == Id);
        }

        public async Task<IEnumerable<Deal>> GetByName(string Name)
        {
            return await _context.Deals
                           .Where(d => d.Title.ToLower().Contains(Name.ToLower())).ToListAsync();
        }

        //Get by Status : Active, Pending, Expired based on Const.cs
        public async Task<IEnumerable<Deal>> GetByStatus(int status)
        {
            return await _context.Deals
                                 .OrderByDescending(t => t.CreatedTS)
                                 .Where(s => s.Status.Equals(status)).ToListAsync();

        }

        public async Task<IEnumerable<Deal>> GetAllByCategory(long Id)
        {
            return await _context.Deals
                           .OrderByDescending(t => t.CreatedTS)
                           .Where(c => c.DealCategoryId.Equals(Id)).ToListAsync();
        }

        public async Task<IEnumerable<Deal>> GetAllBySearchString(string SearchString)
        {
            var ss = SearchString.ToLower();
            return await _context.Deals
                           .OrderByDescending(t => t.CreatedTS)
                           .Where(s => s.Title.ToLower().Contains(ss)
                                  || s.Instructions.ToLower().Contains(ss)).ToListAsync(); 
        }


        public async Task<IEnumerable<Deal>> GetAllPopular(int Views)
        {
            return await _context.Deals
                                   .OrderByDescending(t => t.CreatedTS)
                                   .Where(s => s.Status.Equals(c.DealStatus.Active) && s.Views > 10 && s.Likes  > 5 &&  s.Likes/s.Dislikes > 2).ToListAsync();
        }

        public async Task<bool> Exist(long id)
        {
            return await _context.Deals.AnyAsync(c => c.Id == id);
        }

        public async Task<Deal> Add(Deal entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Deal> Update(Deal entity)
        {
            _context.Deals.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<Deal> Remove(long Id)
        {
            var deal = await _context.Deals.SingleOrDefaultAsync(a => a.Id == Id);
            _context.Deals.Remove(deal);
            await _context.SaveChangesAsync();
            return deal;
        }

    }
}
