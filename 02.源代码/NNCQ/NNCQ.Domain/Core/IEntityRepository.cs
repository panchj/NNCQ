using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Core
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll();
        T GetSingle(Guid id);
        T GetSingleBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector);
        PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
        void DeleteGraph(T entity);
        void Save();
        void SaveOrUpdate(T entity);

        IQueryable<T1> GetAllIncludingRelevance<T1>(params Expression<Func<T1, object>>[] includeProperties);
        IQueryable<T1> GetAllRelevance<T1>();
        T1 GetSingleRelevance<T1>(Guid id);
        T1 GetSingleRelevanceBy<T1>(Expression<Func<T1, bool>> predicate);
        IQueryable<T1> GetMultiRelevanceBy<T1>(Expression<Func<T1, bool>> predicate);
        void SaveRelevance<T1>(T1 entity);
        void EditRelevance<T1>(T1 entity);
    }
}
