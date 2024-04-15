using System.ComponentModel.DataAnnotations;
using TarefasApi.Core.Validation;

namespace TarefasApi.Core.Model
{
    public class TaskEditTaskModel
    {
        public int Codigo { get; set; }
        
        public string? Descricao { get; set; }        
        
        public DateTime? Data { get; set; }

        public string? Status { get; set; }
    }
}
