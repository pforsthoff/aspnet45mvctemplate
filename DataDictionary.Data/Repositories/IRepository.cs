using DataDictionary.ServiceModel.Entities.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataDictionary.Data.Repositories
{
    public interface IRepository : IDisposable
    {
        #region Public Methods and Operators

        void Add<TModel>(TModel instance) where TModel : class, IEntity;

        void Add<TModel>(IEnumerable<TModel> instances) where TModel : class, IEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="useCache">If results are to be stored in the ObjectCache</param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<TModel> All<TModel>(params string[] includeProperties)
            where TModel : class, IEntity;

        void Delete<TModel>(object key) where TModel : class, IEntity;

        void Delete<TModel>(TModel instance) where TModel : class, IEntity;

        void Delete<TModel>(Expression<Func<TModel, bool>> predicate)
            where TModel : class, IEntity;

        IQueryable<TModel> Query<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includeProperties)
            where TModel : class, IEntity;

        TModel Single<TModel>(object key) where TModel : class, IEntity;

        TModel Single<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includeProperties)
            where TModel : class, IEntity;

        TModel SaveChanges<TModel>(object key) where TModel : class, IEntity;

        void SaveChanges();

        DbContext Context { get; }
        IEnumerable<TModel> RawSql<TModel>(string sql) where TModel : class, IEntity;
        IEnumerable<TModel> ExecWithStoredProcedure<TModel>(string query, params object[] parameters) where TModel : class, IEntity;
        void InsertOrUpdate<T>(T entity) where T : class;
        void RemoveRange<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includeProperties)
            where TModel : class, IEntity;
        //JsonResultMessage ExecuteOracleStoredProcedure(string query, params object[] parameters);
        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;
        #endregion
    }
}
