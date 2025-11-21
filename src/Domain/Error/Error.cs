using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Error
{
    public static class Error
    {
        public static Exception ParseError(string message, int statusCode, Exception innerMessage = null!)
        {
            var exception = new Exception(message, innerMessage);
            exception.Data["StatusCode"] = statusCode.ToString();

            return exception;
        }
    }
}