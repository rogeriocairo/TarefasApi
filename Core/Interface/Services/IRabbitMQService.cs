using TarefasApi.Core.Model;

namespace TarefasApi.Core.Interface.Services
{
    public interface IRabbitMQService
    {
        bool SendMessage(TaskModel entity);
        void StartListening(Action<string> messageHandler);
        void Dispose();
    }
}
