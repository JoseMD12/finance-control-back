using System.Net;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Application.Utils
{
    public class ParseError
    {
        public static ActionResult Execute(Error e)
        {
            ActionResult result = e.CodeValue switch
            {
                HttpStatusCode.BadRequest => new BadRequestObjectResult(e.MessageValue),
                HttpStatusCode.NotFound => new NotFoundObjectResult(e.MessageValue),
                HttpStatusCode.Forbidden => new ForbidResult(e.MessageValue),
                HttpStatusCode.Unauthorized => new UnauthorizedResult(),
                HttpStatusCode.NoContent => new NoContentResult(),
                HttpStatusCode.InternalServerError => new ObjectResult(e) { StatusCode = (int)HttpStatusCode.InternalServerError, Value = e.MessageValue },
                _ => new BadRequestObjectResult(e),
            };
            return result;
        }
    }
}