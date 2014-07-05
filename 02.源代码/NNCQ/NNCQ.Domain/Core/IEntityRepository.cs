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

        void Save();
        /// <summary>
        /// 无限制提取所有业务对象
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// 除了提取本身的对象数据集合外，还提取包含根据表达式提取关联的的对象的集合，
        /// </summary>
        /// <param name="includeProperties">需要直接提取关联类集合数据的表达式集合，通过逗号隔开</param>
        /// <returns></returns>
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        T GetSingle(Guid id);
        T GetSingleBy(Expression<Func<T, bool>> predicate);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector);
        PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        
        void Add(T entity);
        void AddAndSave(T entity);


        void Edit(T entity);
        void EditAndSave(T entity);

        void AddOrEdit(T entity);
        void AddOrEditAndSave(T entity);

        void Delete(T entity);
        void DeleteAndSave(T entity);

        void DeleteGraph(T entity);
        void DeleteGraphAndSave(T entity);

        IQueryable<T1> GetAllRelevance<T1>();
        IQueryable<T1> GetAllIncludingRelevance<T1>(params Expression<Func<T1, object>>[] includeProperties);

        IQueryable<T1> FindRelevanceBy<T1>(Expression<Func<T1, bool>> predicate);

        T1 GetSingleRelevance<T1>(Guid id);
        T1 GetSingleRelevanceBy<T1>(Expression<Func<T1, bool>> predicate);

        void AddRelevance<T1>(T1 entity);
        void AddAndSaveRelevance<T1>(T1 entity);

        void EditRelevance<T1>(T1 entity);
        void EditAndSaveRelevance<T1>(T1 entity);

        void DeleteRelevance<T1>(T1 entity);
        void DeleteAndSaveRelevance<T1>(T1 entity);

    }
}
