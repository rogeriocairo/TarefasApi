namespace TarefasApi.Core.Interface.Services
{
    public interface IReceptorRabbitMQ
    {
        public Task StartAsync(CancellationToken cancellationToken);

        public Task StopAsync(CancellationToken cancellationToken);
    }
}
