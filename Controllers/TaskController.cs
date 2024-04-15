using Microsoft.AspNetCore.Mvc;
using TarefasApi.Core.Interface.Services;
using TarefasApi.Core.Model;

namespace TarefasApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TaskController : Controller
    {
        public readonly ITaskService _taskService;
        public readonly IRabbitMQService _rabbitMQService;

        public TaskController(ITaskService taskService, IRabbitMQService rabbitMQService)
        {
            _taskService = taskService;
            _rabbitMQService = rabbitMQService;
        }

        // GET: api/tasks
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetTasks()
        {
            var _apiResponse = new ApiResponseModel<IEnumerable<TaskModel>>();

            try
            {
                _apiResponse.Result = await _taskService
                                            .GetAllAsync()
                                            .ConfigureAwait(false);

                return StatusCode(StatusCodes.Status200OK, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Success = false;
                _apiResponse.Message = ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, _apiResponse);
            }
        }

        // POST: api/AddTask
        [HttpPost("Add")]       
        public IActionResult AddTask([FromBody] TaskModel taskModel)
        {
            var _apiResponse = new ApiResponseModel<TaskModel>();

            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Dados do modelo inválidos. Por favor, verifique a entrada.");

                _rabbitMQService.SendMessage(taskModel);
                _apiResponse.Message = "Tarefa Criada com sucesso!";
                _apiResponse.Result = taskModel;

                return StatusCode(StatusCodes.Status201Created, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Success = false;
                _apiResponse.Message = ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, _apiResponse);
            }
        }

        // GET: api/tasks
        [HttpGet("GetById")]
        public async Task<IActionResult> GetTask(int idTarefa)
        {
            var _apiResponse = new ApiResponseModel<TaskModel>();

            try
            {
                _apiResponse.Result = await _taskService
                                            .GetByIdAsync(idTarefa)
                                            .ConfigureAwait(false);

                return StatusCode(StatusCodes.Status200OK, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Success = false;
                _apiResponse.Message = ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, _apiResponse);
            }
        }

        // GET: api/task/Edit/5
        [HttpPut("EditTask")]        
        public IActionResult EditTask([FromBody] TaskEditTaskModel taskEditTaskModel)
        {
            var _apiResponse = new ApiResponseModel<bool>();

            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Dados do modelo inválidos. Por favor, verifique a entrada.");

                var _taskModel = new TaskModel()
                {
                    Codigo = taskEditTaskModel.Codigo,
                    Descricao = taskEditTaskModel.Descricao,
                    Status = taskEditTaskModel.Status,
                };

                _apiResponse.Result = _rabbitMQService.SendMessage(_taskModel);
                _apiResponse.Message = "Tarefa Atualizada com sucesso!";

                return StatusCode(StatusCodes.Status200OK, _apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.Success = false;
                _apiResponse.Message = ex.Message;
                return StatusCode(StatusCodes.Status400BadRequest, _apiResponse);
            }
        }
    }
}
