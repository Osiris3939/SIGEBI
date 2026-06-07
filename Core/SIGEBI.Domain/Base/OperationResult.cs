namespace SIGEBI.Domain.Base
{
    // Representa el resultado de una operacion basica
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}
