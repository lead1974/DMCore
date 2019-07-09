using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using DMCore.Data.Core.Repositories;
using DMCore.Data.Repositories;

namespace DMCore.Data.Persistance.Repositories
{
    public class DealCategoryRepository : Repository<DealCategory>, IDealCategoryRepository
    {
        public DealCategoryRepository(DMDbContext context) : base(context)
        {
        }
    }
}
