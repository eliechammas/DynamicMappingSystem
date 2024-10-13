
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace DynamicMapping.Infrastructure
{
    public sealed class ExceptionHandler(ILogger<ExceptionHandler> logger): IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger = logger;

        public async ValueTask<Boolean> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = exception switch
            {
                Microsoft.AspNetCore.Http.BadHttpRequestException badRequestException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = badRequestException.GetType().Name,
                    Title = "Unexpected Error Occured",
                    Detail = badRequestException.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                    Extensions = new Dictionary<string, object?>
                    {
                        //{"errors", badRequestException.Errors }
                    }
                },
                ArgumentNullException argumentNullException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = argumentNullException.GetType().Name,
                    Title = "Unexpected Error Occured",
                    Detail = argumentNullException.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                },
                ArgumentOutOfRangeException argumentOutOfRangeException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = argumentOutOfRangeException.GetType().Name,
                    Title = "Unexpected Error Occured",
                    Detail = argumentOutOfRangeException.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                },
                InvalidCastException invalidCastException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = invalidCastException.GetType().Name,
                    Title = "Unexpected Error Occured",
                    Detail = invalidCastException.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                },
                InvalidDataException invalidDataException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = invalidDataException.GetType().Name,
                    Title = "Unexpected Error Occured",
                    Detail = invalidDataException.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                },
                InvalidOperationException invalidOperationException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = invalidOperationException.GetType().Name,
                    Title = "Unexpected Error Occured",
                    Detail = invalidOperationException.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                },
                OverflowException overflowException => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = overflowException.GetType().Name,
                    Title = "Unexpected Error Occured",
                    Detail = overflowException.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                },
                _ => new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = exception.GetType().Name,
                    Title = "Unexpected Error Occured",
                    Detail = exception.Message,
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                }
            };

            //Log the exception
            _logger.LogError("Unhandled exception occurred while executing request: {ex}", problemDetails);

            //Serilog Logging
            Serilog.Log.Error("ERROR TRACE: " + problemDetails.Status + " - " + problemDetails.Type + Environment.NewLine);
            Serilog.Log.Error("Error Message: " + Environment.NewLine + problemDetails.Detail);
            Serilog.Log.Information("Instance: " + problemDetails.Instance);

            httpContext.Response.StatusCode = problemDetails.Status!.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }
    }
}

//public async ValueTask<Boolean> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
//{
//    var result = new ProblemDetails();

 
//    switch (exception)
//    {
//        case Microsoft.AspNetCore.Http.BadHttpRequestException badRequestException:
//            result = new ProblemDetails
//            {
//                Status = (int)HttpStatusCode.BadRequest,
//                Type = badRequestException.GetType().Name,
//                Title = "Unexpected Error Occured",
//                Detail = badRequestException.Message,
//                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
//            };
//            break;

//        case ArgumentNullException argumentNullException:
//            result = new ProblemDetails
//            {
//                Status = (int)HttpStatusCode.NotFound,
//                Type = argumentNullException.GetType().Name,
//                Title = "Unexpected Error Occured",
//                Detail = argumentNullException.Message,
//                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
//            };
//            break;

//        case ArgumentOutOfRangeException argumentOutOfRangeException:
//            result = new ProblemDetails
//            {
//                Status = (int)HttpStatusCode.NotFound,
//                Type = argumentOutOfRangeException.GetType().Name,
//                Title = "Unexpected Error Occured",
//                Detail = argumentOutOfRangeException.Message,
//                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
//            };
//            break;

//        default:
//            result = new ProblemDetails
//            {
//                Status = (int)HttpStatusCode.InternalServerError,
//                Type = exception.GetType().Name,
//                Title = "Unexpected Error Occured",
//                Detail = exception.Message,
//                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
//            };
//            break;
//    }
//    await httpContext.Response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);

    //await Results.Problem(
    //            statusCode: (int)HttpStatusCode.InternalServerError,
    //            type: exception.GetType().Name,
    //            title: "Unexpected Error Occured",
    //            detail: exception.Message,
    //            instance: $"{httpContext.Request.Method} {httpContext.Request.Path}"
    //            ).ExecuteAsync(httpContext);

 //      return true;
//}