using Stocks.Core.Repository;
using Stocks.Infra.Data.Context;
using TarefasApi.Core.Entity;
using TarefasApi.Core.Interface.Repository;

namespace TarefasApi.Core.Repository
{
    public class TaskRepository : Repository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}
