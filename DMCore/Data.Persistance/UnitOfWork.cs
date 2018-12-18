using DMCore.Data.Core;
using DMCore.Data.Core.Repositories;
using DMCore.Data.Persistance.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DMDbContext _context;

        public UnitOfWork(DMDbContext context)
        {
            _context = context; 
        }

        public DMDbContext Context { get; }

        public IDealRepository Deals { get; private set; }

        public ICouponRepository Coupons { get; private set; }

        public IStoreRepository Stores { get; private set; }
        public IDealTagRepository TagDeals { get; private set; }

        public IDealCategoryRepository DealCategories { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
