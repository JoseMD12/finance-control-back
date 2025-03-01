
using System.Net;

namespace Domain.Dtos
{
    public readonly struct Error(HttpStatusCode code, string message)
    {
        private HttpStatusCode Code { get; } = code;
        private string Message { get; } = message;

        public override string ToString() => $"{Code}: {Message}";
        public static Error BadRequest(string message) => new(HttpStatusCode.BadRequest, message);
        public static Error NotFound(string message) => new(HttpStatusCode.NotFound, message);
        public static Error InternalServerError(string message) => new(HttpStatusCode.InternalServerError, message);
        public static Error Unauthorized(string message) => new(HttpStatusCode.Unauthorized, message);
        public static Error Forbidden(string message) => new(HttpStatusCode.Forbidden, message);

        public string MessageValue => Message;
        public HttpStatusCode CodeValue => Code;
    }
}