namespace TarefasApi.Core.Interface.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity _entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task UpdateAsync(TEntity _entity, int id);
        Task DeleteAsync(int id);        
    }
}
