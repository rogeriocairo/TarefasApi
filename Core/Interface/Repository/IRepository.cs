namespace TarefasApi.Core.Interface.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    public Task AddAsync(TEntity entity);

    public Task UpdateAsync(TEntity entity, int id);

    public Task DeleteAsync(TEntity entity);

    public Task<IEnumerable<TEntity>> GetAll();

    public Task<TEntity?> GetById(int id);
}