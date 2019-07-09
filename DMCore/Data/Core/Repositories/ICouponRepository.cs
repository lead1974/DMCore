using DMCore.Data.Core.Domain.Deal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DMCore.Data.Core.Repositories
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        IEnumerable<Coupon> GetAllPopular(int Views);
        IEnumerable<Coupon> GetAllCouponsPaging(int pageIndex, int pageSize);
    }
}
