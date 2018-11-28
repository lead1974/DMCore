using DMCore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DMCore.Data.Repositories
{
    public interface IEntityRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllByCategory(long id);
        Task<IEnumerable<T>> GetByStatus(int status);
        Task<IEnumerable<T>> GetByName(string name);
        Task<T> GetById(long id);
        Task<bool> Exist(long id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Remove(long id);
        //IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
    }
    public interface IUserRepository<T> : IEntityRepository<AuthUser>
    {
    }
    public interface IDealRepository : IEntityRepository<Deal>
    {
        Task<IEnumerable<Deal>> GetAllBySearchString(string SearchString);
        Task<IEnumerable<Deal>> GetAllPopular(int Views);
    }

    public interface IDealCategoryRepository : IEntityRepository<DealCategory>
    {
    }
    public interface ICouponRepository : IEntityRepository<Coupon>
    {
        Task<IEnumerable<Coupon>> GetAllBySearchString(string SearchString);
        Task<IEnumerable<Coupon>> GetAllPopular(int Views);
    }
    public interface ICouponCategoryRepository : IEntityRepository<CouponCategory>
    {
    }

    public interface IStoreRepository : IEntityRepository<Store>
    {
    }

    public interface IStoreCategoryRepository : IEntityRepository<StoreCategory>
    {
    }
}
