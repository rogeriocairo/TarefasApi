using System.ComponentModel.DataAnnotations;
using TarefasApi.Core.Validation;

namespace TarefasApi.Core.Model
{
    public class TaskModel 
    {
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        [DataType(DataType.Date)]
        [CustomDateValidation(ErrorMessage = "A data deve ser maior ou igual à data de hoje.")]
        public DateTime Data { get; set; }
                
        public string? Status { get; set; }
    }
}
