using IWantApp.DTOs.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using IWantApp.Services.Exceptions;

namespace IWantApp.Controllers.Exceptions;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var requestPath = context.HttpContext.Request.Path;
        var exception = context.Exception;

        if (exception is ResourceNotFoundException)
        {
            context.Result = BuildErrorResponse(HttpStatusCode.NotFound, exception.Message, requestPath);
        }
        else if (exception is DatabaseException)
        {
            context.Result = BuildErrorResponse(HttpStatusCode.Conflict, exception.Message, requestPath);
        }
        else if (exception is UnauthorizedAccessException)
        {
            context.Result = BuildErrorResponse(HttpStatusCode.Unauthorized, exception.Message, requestPath);
        }
        else if (exception is ForbiddenException)
        {
            context.Result = BuildErrorResponse(HttpStatusCode.Forbidden, exception.Message, requestPath);
        }
        else if (exception is UniqueConstraintViolationException)
        {
            context.Result = BuildErrorResponse(HttpStatusCode.Conflict, exception.Message, requestPath);
        }
        else if (exception is ValidationException validationException)
        {
            var validationError = new ValidationError((int)HttpStatusCode.UnprocessableEntity, "Dados inválidos", requestPath)
            {
                Errors = validationException.Errors
            };
            context.Result = new JsonResult(validationError) { StatusCode = (int)HttpStatusCode.UnprocessableEntity };
        }
        else
        {
            context.Result = BuildErrorResponse(HttpStatusCode.InternalServerError, "Erro interno do servidor: " + exception.Message, requestPath);
        }

        context.ExceptionHandled = true;
    }

    private JsonResult BuildErrorResponse(HttpStatusCode status, string message, string path)
    {
        var error = new CustomErrorDTO((int)status, message, path);
        return new JsonResult(error) { StatusCode = (int)status };
    }
}
