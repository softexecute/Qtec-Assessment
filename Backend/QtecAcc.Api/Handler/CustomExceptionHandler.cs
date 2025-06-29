using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace QtecAcc.Api.Handler
{

        public class CustomExceptionHandler(ILogger<CustomExceptionHandler> _logger) : IExceptionHandler
        {
            public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
            {
                _logger.LogError(exception, exception.Message);
                var statusCode = 400;
                var title = "Error";
                switch (exception)
                {
                    case NotImplementedException:
                        title = "Not Implemented";
                        statusCode = StatusCodes.Status501NotImplemented;
                        break;
                    case DbException:
                        title = "Database Error";
                        statusCode = StatusCodes.Status503ServiceUnavailable;
                        break;
                    case UnauthorizedAccessException:
                        title = "Unauthorized";
                        statusCode = StatusCodes.Status401Unauthorized;
                        break;
                    case ArgumentNullException:
                        title = "Bad Request";
                        statusCode = StatusCodes.Status400BadRequest;
                        break;
                    case ValidationException:
                        title = "Validation Error";
                        statusCode = StatusCodes.Status422UnprocessableEntity;
                        break;
                    case KeyNotFoundException:
                        title = "Not Found";
                        statusCode = StatusCodes.Status404NotFound;
                        break;
                    default:
                        title = "Internal Server Error";
                        statusCode = StatusCodes.Status500InternalServerError;
                        break;
                }
                var response = new
                {
                    Message = exception.Message,
                    StatusCode = statusCode,
                    title = title,
                    InnerException = exception.InnerException
                };
                httpContext.Response.StatusCode = statusCode;
                await httpContext.Response.WriteAsJsonAsync(response);
                return true;
            }
        }
}
