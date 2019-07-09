using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using DMCore.Data.Core.Repositories;
using DMCore.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DMCore.Data.Persistance.Repositories
{
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        public CouponRepository(DMDbContext context) : base(context)
        {
        }

        public IEnumerable<Coupon> GetAllCouponsPaging(int pageIndex, int pageSize = 20)
        {
            return  _context.Coupons
                            .Include(c => c.DealCategory)
                            .OrderByDescending(c => c.CreatedTS)
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
        }

        public IEnumerable<Coupon> GetAllPopular(int Views)
        {
            return  _context.Coupons
                       .OrderByDescending(t => t.CreatedTS)
                       .Where(s => s.Status.Equals(c.Status.Active.ToString()) && s.Views > 10 && s.Likes > 5 && s.Likes / s.Dislikes > 2).ToList();
        }

    }
}
