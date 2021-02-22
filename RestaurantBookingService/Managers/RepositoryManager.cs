using Microsoft.EntityFrameworkCore;
using RestaurantBookingService.DataAccess.Context;
using RestaurantBookingService.DataAccess.Models;
using RestaurantBookingService.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantBookingService.Managers
{
    public class RepositoryManager : IRepository
    {
        protected readonly Context _dbContext;
        private readonly FactoryManager _factory;

        public RepositoryManager(Context context, FactoryManager factoryManager)
        {
            _dbContext = context;
            _factory = factoryManager;
        }

        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : BaseEntity
        {
            var dbSet = _dbContext.Set<T>();
            return await dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken) where T : BaseEntity
        {
            var dbSet = _dbContext.Set<T>();
            return await dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<List<T>> GetAllAsync<T>(CancellationToken cancellationToken) where T : BaseEntity
        {
            var dbSet = _dbContext.Set<T>();
            return await dbSet.ToListAsync(cancellationToken);
        }

        public T GetManagerInstance<T>() where T : BaseManager, new()
        {
            return _factory.CreateManager<T>(_dbContext);
        }

        public void Update<T>(T entity) where T : BaseEntity
        {
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            var dbSet = _dbContext.Set<T>();
            dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
