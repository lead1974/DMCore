using DMCore.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DMCore.Data.Core.Repositories
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        Task<IEnumerable<Coupon>> GetAllPopular(int Views);
        Task<IEnumerable<Coupon>> GetCouponsPaging(int pageIndex, int pageSize);
    }
}
