using TarefasApi.Core.Entity;

namespace Stocks.Core.Entity
{
    public class LogEntity : BaseEntity
    {
        public LogEntity(int severidade, string message)
        {
            Severidade = severidade;
            Message = message;
        }

        public int Severidade { get; private set; }

        public string Message { get; private set; }

    }
}
