using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DMCore.Data.Models;
using Microsoft.AspNetCore.Identity;
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

        public IEnumerable<Deal> GetAll()
        {
           try
            {
                return _context.Deals.OrderByDescending(t => t.CreatedTS);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get deals from database", ex);
                return null;
            }
}

        public Deal FindById(long Id)
        {
            return _context.Deals
                           .Where(d => d.Id == Id)
                           .FirstOrDefault();
        }

        public Deal FindByName(string Name)
        {
            return _context.Deals
                           .Where(d => d.Title.ToLower().Contains(Name.ToLower()))
                           .FirstOrDefault();
        }

        public IEnumerable<Deal> GetAllActive()
        {
            return _context.Deals
                           .OrderByDescending(t => t.CreatedTS)
                           .Where(s => s.Status.Equals(c.DealStatus.Active));

        }

        public IEnumerable<Deal> GetAllExpired()
        {
            return _context.Deals
                           .OrderByDescending(t => t.CreatedTS)
                           .Where(s => s.Status.Equals(c.DealStatus.Expired));
        }
        public IEnumerable<Deal> GetAllPending()
        {
            return _context.Deals
                           .OrderByDescending(t => t.CreatedTS)
                           .Where(s => s.Status.Equals(c.DealStatus.Pending));
        }

        public IEnumerable<Deal> GetAllByCategory(long Id)
        {
            return _context.Deals
                           .OrderByDescending(t => t.CreatedTS)
                           .Where(c => c.DealCategoryId.Equals(Id));
        }

        public IEnumerable<Deal> GetAllBySearchString(string SearchString)
        {
            var ss = SearchString.ToLower();
            return _context.Deals
                           .OrderByDescending(t => t.CreatedTS)
                           .Where(s => s.Title.ToLower().Contains(ss)
                                  || s.Instructions.ToLower().Contains(ss));
        }

        public IEnumerable<Deal> GetAllIncluding(params Expression<Func<Deal, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Deal> GetAllPopular(int Views)
        {
            return _context.Deals
               .OrderByDescending(t => t.CreatedTS)
               .Where(s => s.Status.Equals(c.DealStatus.Active) && s.Views > 10 && s.Likes  > 5 &&  s.Likes/s.Dislikes > 2);
        }

        public void Add(Deal entity)
        {
            _context.Add(entity);

        }
        public void Remove(Deal entity)
        {
            _context.Remove(entity);
        }

        public void Update(Deal entity)
        {
            _context.Update(entity);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
