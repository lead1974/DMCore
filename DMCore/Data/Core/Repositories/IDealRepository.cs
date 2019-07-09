using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DMCore.Data.Core.Repositories
{
    public interface IDealRepository : IRepository<Deal>
    {
        IEnumerable<Deal> GetAllPopular(int Views);
        IEnumerable<Deal> GetAllDealsPaging(int pageIndex, int pageSize);
    }
}
