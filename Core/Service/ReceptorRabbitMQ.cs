using Newtonsoft.Json;
using TarefasApi.Core.Interface.Services;
using TarefasApi.Core.Model;

namespace TarefasApi.Core.Service
{
    public class ReceptorRabbitMQ : IReceptorRabbitMQ
    {
        public const string QUEUE_NAME = "tarefas";
        public IRabbitMQService _rabbitMQService;
        public readonly ITaskService _taskService;

        public ReceptorRabbitMQ(
            IRabbitMQService rabbitMQService,
            ITaskService taskService)
        {
            _rabbitMQService = rabbitMQService;
            _taskService = taskService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Rodando");

            _rabbitMQService.StartListening(message =>
            {
                Console.WriteLine($"Received message: {message}");

                var tarefa = JsonConvert.DeserializeObject<TaskModel>(message);

                if (tarefa != null)
                {
                    if (tarefa.Codigo > 0)
                    {
                        _taskService.UpdateAsync(tarefa, (int)tarefa.Codigo);
                    }
                    else
                    {
                        _taskService.AddAsync(tarefa);
                    }
                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Receptor RabbitMQ is stopping.");
            return Task.CompletedTask;
        }
    }
}