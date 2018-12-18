using DMCore.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DMCore.Data.Core.Repositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<IEnumerable<Store>> GetStorePaging(int pageIndex, int pageSize);
    }
}
