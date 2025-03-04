using System.Net;
using System.Text.Json;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Application.Utils
{
    public class ParseError
    {
        public static ActionResult Execute(Error e)
        {
            var settings = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            var message = JsonSerializer.Serialize(new { message = e.MessageValue }, settings);

            ActionResult result = e.CodeValue switch
            {
                HttpStatusCode.BadRequest => new BadRequestObjectResult(message),
                HttpStatusCode.NotFound => new NotFoundObjectResult(message),
                HttpStatusCode.Forbidden => new ForbidResult(message),
                HttpStatusCode.Unauthorized => new UnauthorizedResult(),
                HttpStatusCode.NoContent => new NoContentResult(),
                HttpStatusCode.InternalServerError => new ObjectResult(e)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Value = message,
                },
                _ => new BadRequestObjectResult(e),
            };
            return result;
        }
    }
}
