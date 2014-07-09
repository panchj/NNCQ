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

        /// <summary>
        /// 根据对象的ID提取具体的对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetSingle(Guid id);

        /// <summary>
        /// 根据 Lambda 表达式提取具体的对象，实际上是提取满足表达式限制的集合的第一个对象集合
        /// </summary>
        /// <param name="predicate">布尔条件的 Lambda 表达式</param>
        /// <returns></returns>
        T GetSingleBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据 Lambda 表达式提取对象集合
        /// </summary>
        /// <param name="predicate">布尔条件的 Lambda 表达式</param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 按照指定的属性进行分页，提取分页后的对象集合，在本框架中，通常使用 SortCode
        /// </summary>
        /// <typeparam name="TKey">分页所依赖的属性</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页对象的数量</param>
        /// <param name="keySelector">指定分页依赖属性的 Lambda 表达式</param>
        /// <returns></returns>
        PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector);

        /// <summary>
        /// 按照指定的属性进行分页，提取分页后的对象的集合
        /// </summary>
        /// <typeparam name="TKey">分页所依赖的属性</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页对象的数量</param>
        /// <param name="keySelector">指定分页依赖属性的 Lambda 表达式</param>
        /// <param name="predicate">对象集合过滤 Lambda 表达式</param>
        /// <param name="includeProperties">指定的扩展对象属性的表达式集合，通过逗号隔离</param>
        /// <returns></returns>
        PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        
        /// <summary>
        /// 添加对象到内存中的数据集中
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// 添加对象到内存中的数据集中，并直接持久化。
        /// </summary>
        /// <param name="entity"></param>
        void AddAndSave(T entity);

        /// <summary>
        /// 编辑内存中对应的数据集的对象
        /// </summary>
        /// <param name="entity"></param>
        void Edit(T entity);

        /// <summary>
        /// 编辑内存中对应的数据集的对象，并直接持久化。
        /// </summary>
        /// <param name="entity"></param>
        void EditAndSave(T entity);

        /// <summary>
        /// 根据内存中对应的数据集是否存在，自动决定采取添加或者编辑方法处理传入的对象
        /// </summary>
        /// <param name="entity"></param>
        void AddOrEdit(T entity);

        /// <summary>
        /// 根据内存中对应的数据集是否存在，自动决定采取添加或者编辑方法处理传入的对象，并直接持久化。
        /// </summary>
        /// <param name="entity"></param>
        void AddOrEditAndSave(T entity);

        /// <summary>
        /// 删除内存中对应的数据集的对象。
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 删除内存中对应的数据集的对象，并直接持久化。
        /// </summary>
        /// <param name="entity"></param>
        void DeleteAndSave(T entity);

        /// <summary>
        /// 删除的另外一种方法
        /// </summary>
        /// <param name="entity"></param>
        void DeleteGraph(T entity);
        void DeleteGraphAndSave(T entity);

        #region 以下是针对在当前的 Repository 中需要在同一个 EF 的上下文实例中进行处理的关联对象的处理方法完全，具体使用与上面的方式一样
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
        #endregion

    }
}
