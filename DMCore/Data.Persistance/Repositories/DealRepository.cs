using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Repositories;
using DMCore.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DMCore.Data.Persistance.Repositories
{
    public class DealRepository : Repository<Deal>, IDealRepository
    {
        public DealRepository(DMDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Deal>> GetAllDealsPaging(int pageIndex, int pageSize = 20)
        {
            return await _context.Deals
                            .Include(c => c.DealCategory)
                            .OrderByDescending(c => c.CreatedTS)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Deal>> GetAllPopular(int Views)
        {
            return await _context.Deals
                       .OrderByDescending(t => t.CreatedTS)
                       .Where(s => s.Status.Equals(c.Status.Active.ToString()) && s.Views > 10 && s.Likes > 5 && s.Likes / s.Dislikes > 2).ToListAsync();
        }
    }
}
