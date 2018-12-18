using DMCore.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DMCore.Data.Core.Repositories
{
    public interface IDealRepository : IRepository<Deal>
    {
        Task<IEnumerable<Deal>> GetAllPopular(int Views);
        Task<IEnumerable<Deal>> GetAllDealsPaging(int pageIndex, int pageSize);
    }
}
