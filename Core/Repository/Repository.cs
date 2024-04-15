using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using Stocks.Infra.Data.Context;
using System.Text;
using System.Text.Json;
using TarefasApi.Core.Interface.Repository;


namespace Stocks.Core.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _dataContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = dataContext.Set<TEntity>();
        }
        
        public async Task AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _dataContext.SaveChangesAsync();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dataContext
                        .Set<TEntity>()
                        .ToListAsync();
        }
        
        public async Task UpdateAsync(TEntity entity, int id)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entityInDb = _dbSet.Find(id);

            if (entityInDb == null)
            {
                throw new Exception("Entity not found in database");
            }                       
            
            _dbSet.Update(entity);
            await _dataContext.SaveChangesAsync();
        }


        public async Task<TEntity?> GetById(int id)
        {
            var _registro = await _dataContext
                       .Set<TEntity>()
                       .FindAsync(id);

            if (_registro != null)
                return _registro;
            else
                return default;
        }      
    }
}
