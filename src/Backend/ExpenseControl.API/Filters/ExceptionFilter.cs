using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyExpenseControl.Communication.Response;
using MyExpenseControl.Exceptions;
using MyExpenseControl.Exceptions.ExceptionsBase;
using System.Net;

namespace ExpenseControl.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ExpenseControlException expenseControl)
            {
                HandleProjectExcpetion(expenseControl, context);
            }
            else
            {
                ThrowUnknowExcpetion(context);
            }
        }
        private static void HandleProjectExcpetion(ExpenseControlException myRecipeBookException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)myRecipeBookException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(myRecipeBookException.GetErrorMessages()));
        }

        private static void ThrowUnknowExcpetion(ExceptionContext context)
        {
            //context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOW_ERROR));
        }
    }
}