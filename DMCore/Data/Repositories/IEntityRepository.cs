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
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAllByCategory(long Id);
        IEnumerable<T> GetAllActive();
        IEnumerable<T> GetAllPending();
        IEnumerable<T> GetAllExpired();
        T FindById(long Id);
        T FindByName(string Name);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        bool SaveAll();
    }
    public interface IUserRepository<T> : IEntityRepository<AuthUser>
    {
    }
    public interface IDealRepository : IEntityRepository<Deal>
    {
        IEnumerable<Deal> GetAllBySearchString(string SearchString);
        IEnumerable<Deal> GetAllPopular(int Views);
    }

    public interface IDealCategoryRepository : IEntityRepository<DealCategory>
    {
    }
    public interface ICouponRepository : IEntityRepository<Coupon>
    {
        IEnumerable<Coupon> GetAllBySearchString(string SearchString);
        IEnumerable<Coupon> GetAllPopular(int Views);
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
