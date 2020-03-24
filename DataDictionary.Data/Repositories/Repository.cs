using DataDictionary.ServiceModel.Entities.Base;
using DataDictionary.ServiceModel.Messaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace DataDictionary.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly DbContext _context;
        private const bool _isSharedContext = false;

        public Repository(DbContext context)
        {
            _context = context;
            //Context.Set<RdmsDescription>().AsNoTracking();
            //Context.Set<UserRoles>().AsNoTracking();
            //Context.Set<User>().AsNoTracking();
            //Context.Set<History>().AsNoTracking();

#if DEBUG
            _context.Database.Log = m => Trace.WriteLine(m);
#endif
        }
        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class, IEntity
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }
        public IEnumerable<TModel> RawSql<TModel>(string sql) where TModel : class, IEntity
        {
            var items = _context.Set<TModel>().SqlQuery(sql);
            return items;
        }

        public IEnumerable<TModel> ExecWithStoredProcedure<TModel>(string query, params object[] parameters) where TModel : class, IEntity
        {
            return _context.Set<TModel>().SqlQuery(query, parameters);
        }

        public DbContext Context
        {
            get { return _context; }
        }

        public void Add<TModel>(TModel instance) where TModel : class, IEntity
        {
            _context.Set<TModel>().Add(instance);

            if (_isSharedContext == false)
                _context.SaveChanges();
        }

        public void Add<TModel>(IEnumerable<TModel> instances) where TModel : class, IEntity
        {
            foreach (var instance in instances)
            {
                Add(instance);
            }
        }

        public IQueryable<TModel> All<TModel>(params string[] includePaths) where TModel : class, IEntity
        {
            //if (useCache)
            //{
            //    var key = "All_{0}".FormatWith(typeof (TModel).Name);
            //    return _cache.GetOrAdd(key, () => Query<TModel>(x => true, includePaths));
            //}
            return Query<TModel>(x => true, includePaths);
        }

        public IRepository CreateRepository()
        {
            return new Repository(_context);
        }

        public void Delete<TModel>(object id) where TModel : class, IEntity
        {
            var instance = Single<TModel>(id);
            Delete(instance);
        }

        public void Delete<TModel>(TModel instance) where TModel : class, IEntity
        {
            if (instance == null) return;
            _context.Set<TModel>().Remove(instance);
            _context.SaveChanges();
        }

        public void Delete<TModel>(Expression<Func<TModel, bool>> predicate) where TModel : class, IEntity
        {
            var entity = Single(predicate);
            Delete(entity);
        }

        public void Dispose()
        {
            // If this is a shared (or null) context then
            // we're not responsible for disposing it
            if (_isSharedContext || _context == null)
            {
                return;
            }

            _context.Dispose();
        }

        public IQueryable<TModel> Query<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includeProperties)
            where TModel : class, IEntity
        {
            var items = GetSetWithIncludedPaths<TModel>(includeProperties);
            //var items = _context.Set<TModel>();
            return predicate != null ? items.Where(predicate) : items;
        }

        public TModel SaveChanges<TModel>(object id) where TModel : class, IEntity
        {
            var instance = _context.Set<TModel>().Find(id);

            _context.SaveChanges();
            return instance;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public TModel Single<TModel>(object id) where TModel : class, IEntity
        {
            var instance = _context.Set<TModel>().Find(id);
            _context.SaveChanges();
            return instance;
        }

        public TModel Single<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includeProperties)
            where TModel : class, IEntity
        {
            TModel instance = null;
            try
            {
                instance = GetSetWithIncludedPaths<TModel>(includeProperties).SingleOrDefault(predicate);
            }
            catch (Exception e)
            {

            }

            return instance;
        }

        private DbQuery<TModel> GetSetWithIncludedPaths<TModel>(IEnumerable<string> includeProperties)
            where TModel : class, IEntity
        {
            DbQuery<TModel> items = _context.Set<TModel>();
            return (includeProperties ?? Enumerable.Empty<string>()).Aggregate(
                items, (current, property) => current.Include(property));
        }
        public void InsertOrUpdate<T>(T entity) where T : class
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _context.Entry(entity).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                _context.Entry(entity).State = EntityState.Unchanged;
                throw;
            }

        }
        public void RemoveRange<TModel>(Expression<Func<TModel, bool>> predicate, params string[] includeProperties)
            where TModel : class, IEntity
        {
            var items = GetSetWithIncludedPaths<TModel>(includeProperties);

            _context.Set<TModel>().RemoveRange(items);
            _context.SaveChanges();
        }
        //public JsonResultMessage ExecuteOracleStoredProcedure(string query, params object[] parameters)
        //{
        //    var result = new JsonResultMessage
        //    {
        //        Success = true,
        //        Message = "Successfully executed procedure."
        //    };
        //    try
        //    {
        //        var results = _context.Database.ExecuteSqlCommand(query, parameters);
        //        var output = new OracleParameter("p_message", OracleDbType.Varchar2, ParameterDirection.Output) { Size = 999000 };
        //        foreach (OracleParameter parameter in parameters)
        //        {
        //            if (parameter.Direction.Equals(ParameterDirection.Output))
        //            {
        //                result.Message = parameter.Value.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Success = false;
        //        result.Message = "Error executing procedure: ." + ex;
        //    }
        //    return result;
        //}
        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).Any();
        }
    }
}
