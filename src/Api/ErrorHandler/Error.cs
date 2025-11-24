
namespace Api.ErrorHandler
{
    public class Error
    {
        public required string Message { get; set; }
        public required int StatusCode { get; set; }
        public string? StackTrace { get; set; }
        public string? InnerException { get; set; }

        public static Error Handler(Exception ex)
        {
            var message = ex.Message ?? "";
            var stackTrace = ex.StackTrace;
            var innerException = ex.InnerException;
            var statusCodeString = ex.Data["StatusCode"] ?? null;
            var statusCode = int.TryParse(statusCodeString?.ToString()?.Trim(), out var code) ? code : 500;

            Console.WriteLine();
            Console.WriteLine("== Handling Error ==");
            Console.WriteLine("Error Message: " + message);
            Console.WriteLine("Stack Trace: " + stackTrace);
            if (innerException != null)
            {
                Console.WriteLine("Inner Exception: " + innerException);
            }
            Console.WriteLine("Status Code: " + statusCode);

            return new Error
            {
                Message = message,
                StatusCode = statusCode,
                StackTrace = stackTrace,
                InnerException = innerException?.ToString()
            };
        }
    }
}