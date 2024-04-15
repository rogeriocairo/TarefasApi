using TarefasApi.Core.Entity;
using TarefasApi.Core.Interface.Repository;
using TarefasApi.Core.Interface.Services;
using TarefasApi.Core.Model;

namespace TarefasApi.Core.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;        
        }

        public async Task AddAsync(TaskModel taskModel)
        {
            var _entity = new TaskEntity
            {
                Description = taskModel.Descricao ?? "",
                Status = taskModel.Status ?? "",
                Date = (DateTime)taskModel.Data
            };

            await _repository.AddAsync(_entity);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskModel>> GetAllAsync()
        {
            var _itens = await _repository.GetAll();

            return _itens.Select(_item => new TaskModel
            {
                Codigo = _item.Id,
                Descricao = _item.Description,
                Data = _item.Date,
                Status = _item.Status
            });
        }

        public async Task<TaskModel?> GetByIdAsync(int id)
        {
            var _item = await _repository.GetById(id);

            if (_item == null)
                return default;

            return new TaskModel
            {
                Codigo = _item.Id,
                Descricao = _item.Description,
                Data = _item.Date,
                Status = _item.Status
            };
        }        

        public async Task UpdateAsync(TaskModel taskModel, int id)
        {
            try
            {
                var _entity = new TaskEntity
                {
                    Id = id,
                    Description = taskModel.Descricao,
                    Status = taskModel.Status                    
                };

                await _repository.UpdateAsync(_entity, _entity.Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }            
        }      
    }
}
