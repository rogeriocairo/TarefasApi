namespace TarefasApi.Core.Model
{
    public class ApiResponseModel<T>
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public T? Result { get; set; }
    }
}
