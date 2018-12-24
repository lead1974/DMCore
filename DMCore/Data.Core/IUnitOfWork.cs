using DMCore.Data.Core.Repositories;
using DMCore.Data.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Core
{
    public interface IUnitOfWork : IDisposable
    {
        DMDbContext _context { get; }
        IDealRepository Deals { get; }
        ICouponRepository Coupons { get; }
        IStoreRepository Stores { get; }
        IDealTagRepository TagDeals { get; }
        IDealCategoryRepository DealCategories { get; }
        int Complete();
    }
}
