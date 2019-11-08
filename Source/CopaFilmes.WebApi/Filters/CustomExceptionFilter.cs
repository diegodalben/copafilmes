using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CopaFilmes.WebApi.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, context.Exception.Message);

            var response = context.HttpContext.Response;
            response.StatusCode = (int) HttpStatusCode.InternalServerError;
            response.ContentType = "application/json";
            context.Result = new JsonResult(new {Message = "Ocorreu um erro inesperado. Por favor tente novamente mais tarde."});
        }
    }
}