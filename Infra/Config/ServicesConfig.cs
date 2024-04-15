using Stocks.Infra.Data.Context;
using TarefasApi.Core.Interface.Repository;
using TarefasApi.Core.Interface.Services;
using TarefasApi.Core.Repository;
using TarefasApi.Core.Service;

namespace Stocks.Api.Infra.Config
{
    public static class ServicesConfig
    {
        public static void AddServices (this IServiceCollection services)
        {            
            services.AddTransient<ITaskService, TaskService>();
           
            services.AddScoped<ITaskRepository, TaskRepository>();

            services.AddScoped<IRabbitMQService, RabbitMQService>();            

            services.AddScoped<DataContext>();
        }
    }
}
