﻿using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace Journey.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is JourneyException)
        {
            var journeyException = (JourneyException)context.Exception;
            context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();
            context.Result = new ObjectResult(context.Exception.Message);
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult("Unknown error");
        }
    }
}
