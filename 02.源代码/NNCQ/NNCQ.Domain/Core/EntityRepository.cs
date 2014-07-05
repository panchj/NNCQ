using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Core
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new() 
    {
        readonly DbContext _entitiesContext;

        public EntityRepository(DbContext context)
        {
            _entitiesContext = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _entitiesContext.Set<T>();
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entitiesContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public T GetSingle(Guid id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }

        public T GetSingleBy(Expression<Func<T, bool>> predicate)
        {
            return _entitiesContext.Set<T>().Where(predicate).FirstOrDefault();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _entitiesContext.Set<T>().Where(predicate);
        }

        public virtual PaginatedList<T> Paginate<TKey>(
            int pageIndex,
            int pageSize,
            Expression<Func<T, TKey>> keySelector)
        {
            return Paginate(pageIndex, pageSize, keySelector, null);
        }

        public virtual PaginatedList<T> Paginate<TKey>(
            int pageIndex,
            int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = GetAllIncluding(includeProperties).OrderBy(keySelector);
            query = (predicate == null) ? query : query.Where(predicate);
            return query.ToPaginatedList(pageIndex, pageSize);
        }

        public virtual void Add(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            _entitiesContext.Set<T>().Add(entity);
        }

        public virtual void Edit(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = _entitiesContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void DeleteGraph(T entity)
        {
            DbSet<T> dbSet = _entitiesContext.Set<T>();
            dbSet.Attach(entity);
            dbSet.Remove(entity);
        }

        public virtual void Save()
        {
            _entitiesContext.SaveChanges();
        }

        public virtual void SaveOrUpdate(T entity)
        {
            var p = GetAll().FirstOrDefault(x => x.ID == entity.ID);
            if (p == null)
                Add(entity);
            else
                Edit(entity);
            Save();
        }

        public virtual IQueryable<T1> GetAllIncludingRelevance<T1>(params Expression<Func<T1, object>>[] includeProperties)
        {
            var dbSet = _entitiesContext.Set(typeof(T1));
            var query = dbSet as IQueryable<T1>;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual IQueryable<T1> GetAllRelevance<T1>()
        {
            var dbSet = _entitiesContext.Set(typeof(T1));
            var query = dbSet as IQueryable<T1>;
            return query;
        }

        public virtual T1 GetSingleRelevance<T1>(Guid id)
        {
            var dbSet = _entitiesContext.Set(typeof(T1));
            return (T1)dbSet.Find(id);

        }

        public virtual T1 GetSingleRelevanceBy<T1>(Expression<Func<T1, bool>> predicate)
        {
            var dbSet = _entitiesContext.Set(typeof(T1)) as IQueryable<T1>;
            return (T1)dbSet.Where(predicate).FirstOrDefault();
        }

        public virtual IQueryable<T1> GetMultiRelevanceBy<T1>(Expression<Func<T1, bool>> predicate)
        {
            var dbSet = _entitiesContext.Set(typeof(T1)) as IQueryable<T1>;
            return (IQueryable<T1>)dbSet.Where(predicate);
        }


        public void SaveRelevance<T1>(T1 entity)
        {
            var dbSet = _entitiesContext.Set(typeof(T1));
            dbSet.Add(entity);
            _entitiesContext.SaveChanges();
        }

        public void EditRelevance<T1>(T1 entity)
        {
            var tempBo = Activator.CreateInstance(typeof(T1));
            tempBo = entity;
            var dbSet = _entitiesContext.Set(typeof(T1));
            dbSet.Attach(tempBo);
            _entitiesContext.Entry(tempBo).State = EntityState.Modified;
            _entitiesContext.SaveChanges();

        }
    }
}
