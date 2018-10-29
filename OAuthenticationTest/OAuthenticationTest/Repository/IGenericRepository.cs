using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OAuthenticationTest.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
         int Add(IEnumerable<TEntity> newEntities);
        int Add(TEntity newEntity);
        Task<int> AddAsync(IEnumerable<TEntity> newEntities);
        Task<int> AddAsync(TEntity newEntity);
        IEnumerable<TEntity> All(params string[] includes);
        IEnumerable<TEntity> All(Expression<Func<TEntity, bool>> filter, params string[] includes);
        IQueryable<TEntity> AllQuerable(params string[] includes);
        Task<IEnumerable<TEntity>> AllAsync();
        TEntity Find(params object[] pks);
        Task<TEntity> FindAsync(params object[] pks);
        IEnumerable<TEntity> GetData(Expression<Func<TEntity, bool>> filter, params string[] includes);
        Task<IEnumerable<TEntity>> GetDataAsync(Expression<Func<TEntity, bool>> filter);
        int Remove(IEnumerable<TEntity> removeEntities);
        int Remove(params object[] pks);
        int Remove(TEntity removeEntity);
        Task<int> RemoveAsync(IEnumerable<TEntity> removeEntities);
        Task<int> RemoveAsync(params object[] pks);
        Task<int> RemoveAsync(TEntity removeEntity);
        int RemoveByExpression(Expression<Func<TEntity, bool>> filter);
        int Update(TEntity updateEntity);
        Task<int> UpdateAsync(TEntity updateEntity);
        int GetCount();
        int GetCount(Expression<Func<TEntity, bool>> filter);

        DbContext AddForTransaction(TEntity newEntity, DbContext context);
        int GetCount(Expression<Func<TEntity, bool>> filter, params string[] includes);
    }
}
