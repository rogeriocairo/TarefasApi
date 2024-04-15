using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TarefasApi.Core.Entity;
using TarefasApi.Core.Interface.Services;
using TarefasApi.Core.Model;

namespace TarefasApi.Core.Service
{
    public class RabbitMQService : IRabbitMQService
    {
        private const string _queueName = "taskQueue";
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void StartListening(Action<string> messageHandler)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                messageHandler(message);
            };
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }

        public bool SendMessage(TaskModel entity)
        {
            try
            {
                var message = JsonConvert.SerializeObject(entity);
                var body = Encoding.UTF8.GetBytes(message);
                _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }      
    }
}