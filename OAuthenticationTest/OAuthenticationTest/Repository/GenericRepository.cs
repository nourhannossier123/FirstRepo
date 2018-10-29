using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace OAuthenticationTest.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
       // private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Constructor
        protected readonly Func<DbContext> _dbContextCreator;

        public GenericRepository(Func<DbContext> dbContextCreator)
        {
            if (dbContextCreator == null) throw new ArgumentNullException(nameof(dbContextCreator), "The parameter dbContextCreator can not be null");
            _dbContextCreator = dbContextCreator;

        }
        #endregion

        #region GetAll
        public IEnumerable<TEntity> All(params string[] includes)
        {
            var result = Enumerable.Empty<TEntity>();
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                foreach (string include in includes)
                    query = query.Include(include);

                result = query.ToList();
            }

            //return _mapper.Map<List<TEntity>>(result);
            return result;

        }

        public IQueryable<TEntity> AllQuerable(params string[] includes)
        {
            IQueryable<TEntity> result;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                foreach (string include in includes)
                    query = query.Include(include);

                result = query.ToList().AsQueryable();
                return result;
            }


        }
        public Task<IEnumerable<TEntity>> AllAsync()
        {
            return Task.Run(() =>
            {
                return All();
            });
        }

        #endregion

        #region Find By PrimaryKey
        private TEntity FindInternally(params object[] pks)
        {
            if (pks == null) throw new ArgumentNullException(nameof(pks), "The parameter pks can not be null");
            TEntity result = null;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                result = dbSet.Find(pks);

            }
            return result;
        }
        public TEntity Find(params object[] pks)
        {
            if (pks == null) throw new ArgumentNullException(nameof(pks), "The parameter pks can not be null");
            TEntity result = null;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                result = dbSet.Find(pks);
            }
            return result;
        }
        public Task<TEntity> FindAsync(params object[] pks)
        {
            return Task.Run(() =>
            {
                return Find(pks);
            });
        }
        private IEnumerable<TEntity> FindInternally(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException("The filter can not be null");
            IEnumerable<TEntity> result = null;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                result = GetData(filter);

            }
            return result;
        }
        #endregion

        #region Get By Query
        public IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter), "The parameter filter can not be null");
            var result = Enumerable.Empty<TEntity>();
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                foreach (string include in includes)
                    query = query.Include(include);

                result = query.Where(filter).ToList();
            }
            return result;
        }
        public Task<IEnumerable<TEntity>> GetDataAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Task.Run(() =>
            {
                return GetData(filter);
            });
        }
        #endregion

        #region Add Entity
        public int Add(TEntity newEntity)
        {
            if (newEntity == null) throw new ArgumentNullException(nameof(newEntity), "The parameter newEntity can not be null");
            var result = 0;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                dbSet.Add(newEntity);
                try
                {
                    result = context.SaveChanges();
                }
                catch (Exception ex)
                {
                   // LogManagement.getInstance(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, System.Reflection.MethodBase.GetCurrentMethod()).Error(ex);
                    throw ex;
                }

            }
            return result;
        }
        public Task<int> AddAsync(TEntity newEntity)
        {
            return Task.Run(() =>
            {
                return Add(newEntity);
            });
        }
        public int Add(IEnumerable<TEntity> newEntities)
        {
            if (newEntities == null) throw new ArgumentNullException(nameof(newEntities), "The parameter newEntities can not be null");
            var result = 0;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();

                dbSet.AddRange(newEntities);
                try
                {
                    result = context.SaveChanges();
                }
                catch (Exception ex)
                {
                    //LogManagement.getInstance(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, System.Reflection.MethodBase.GetCurrentMethod()).Error(ex);
                    throw ex;
                }

            }
            return result;
        }
        public Task<int> AddAsync(IEnumerable<TEntity> newEntities)
        {
            return Task.Run(() =>
            {
                return Add(newEntities);
            });
        }
        #endregion

        #region Remove Entity
        /// For Object (TEntity)  
        public int Remove(TEntity removeEntity)
        {
            if (removeEntity == null) throw new ArgumentNullException(nameof(removeEntity), "The parameter removeEntity can not be null");
            var result = 0;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                dbSet.Attach(removeEntity);
                context.Entry(removeEntity).State = EntityState.Deleted;
                try
                {
                    result = context.SaveChanges();
                }
                catch (Exception ex)
                {
                   // LogManagement.getInstance(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, System.Reflection.MethodBase.GetCurrentMethod()).Error(ex);
                    throw ex;
                }

            }
            return result;
        }
        public Task<int> RemoveAsync(TEntity removeEntity)
        {
            return Task.Run(() =>
            {
                return Remove(removeEntity);
            });
        }
        public int Remove(IEnumerable<TEntity> removeEntities)
        {
            if (removeEntities == null) throw new ArgumentNullException(nameof(removeEntities), "The parameter removeEntities can not be null");
            var result = 0;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                foreach (var removeEntity in removeEntities)
                {
                    //var entity = _mapper.Map<TEntity>(removeEntity);
                    dbSet.Attach(removeEntity);
                    context.Entry(removeEntity).State = EntityState.Deleted;
                }
                //var entities = _mapper.Map<IEnumerable<TEntity>>(removeEntities);
                dbSet.RemoveRange(removeEntities);
                try
                {
                    result = context.SaveChanges();
                }
                catch (Exception ex)
                {
                   // LogManagement.getInstance(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, System.Reflection.MethodBase.GetCurrentMethod()).Error(ex);
                    throw ex;
                }

            }
            return result;
        }
        public Task<int> RemoveAsync(IEnumerable<TEntity> removeEntities)
        {
            return Task.Run(() =>
            {
                return Remove(removeEntities);
            });
        }
        /// For PKs  
        public int Remove(params object[] pks)
        {
            if (pks == null) throw new ArgumentNullException(nameof(pks), "The parameter removeEntity can not be null");
            var result = 0;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                var entity = FindInternally(pks);
                dbSet.Attach(entity);
                context.Entry(entity).State = EntityState.Deleted;
                try
                {
                    result = context.SaveChanges();
                }
                catch (Exception ex)
                {
                   // LogManagement.getInstance(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, System.Reflection.MethodBase.GetCurrentMethod()).Error(ex);
                    throw ex;
                }

            }
            return result;
        }
        public Task<int> RemoveAsync(params object[] pks)
        {
            return Task.Run(() =>
            {
                return Remove(pks);
            });
        }

        public int RemoveByExpression(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException("The Filter can not be null");
            var result = 0;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                var entities = FindInternally(filter);
                foreach (var entity in entities)
                {
                    dbSet.Attach(entity);
                    context.Entry(entity).State = EntityState.Deleted;
                    try
                    {
                        result = context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                      //  LogManagement.getInstance(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, System.Reflection.MethodBase.GetCurrentMethod()).Error(ex);
                        throw ex;
                    }
                }
            }
            return result;
        }

        #endregion

        #region Update Entity
        public int Update(TEntity updateEntity)
        {
            if (updateEntity == null) throw new ArgumentNullException(nameof(updateEntity), "The parameter updateEntity can not be null");
            var result = 0;
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                //var entity = _mapper.Map<TEntity>(updateEntity);
                dbSet.Attach(updateEntity);
                context.Entry(updateEntity).State = EntityState.Modified;
                try
                {
                    result = context.SaveChanges();
                }
                catch (Exception ex)
                {
                   // LogManagement.getInstance(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, System.Reflection.MethodBase.GetCurrentMethod()).Error(ex);
                    throw ex;
                }

            }
            return result;
        }
        public Task<int> UpdateAsync(TEntity updateEntity)
        {
            return Task.Run(() =>
            {
                return Update(updateEntity);
            });
        }

        public int GetCount()
        {
            var result = Enumerable.Empty<TEntity>();
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                return dbSet.Count();
            }

        }
        public int GetCount(Expression<Func<TEntity, bool>> filter)
        {
            var result = Enumerable.Empty<TEntity>();
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                return dbSet.Where(filter).Count();
            }
        }

        public IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            var result = Enumerable.Empty<TEntity>();
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                foreach (string include in includes)
                    query = query.Include(include);

                result = query.Where(filter).ToList();
            }
            //return _mapper.Map<List<TEntity>>(result);
            return result;

        }



        #endregion


        public DbContext AddForTransaction(TEntity newEntity, DbContext context)
        {
            if (newEntity == null) throw new ArgumentNullException(nameof(newEntity), "The parameter newEntity can not be null");
            //var result = 0;
            //var context = _dbContextCreator();
            var dbSet = context.Set<TEntity>();
            dbSet.Add(newEntity);

            return context;
        }
        public int GetCount(Expression<Func<TEntity, bool>> filter, params string[] includes)
        {
            var result = Enumerable.Empty<TEntity>();
            using (var context = _dbContextCreator())
            {
                var dbSet = context.Set<TEntity>();
                IQueryable<TEntity> query = dbSet;
                foreach (string include in includes)
                    query = query.Include(include);


                return query.Where(filter).Count();
            }

        }
    }
}