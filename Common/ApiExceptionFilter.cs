using Microsoft.AspNetCore.Mvc.Filters;

namespace Common
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        //public override void OnException(ExceptionContext context)
        //{
        //    if (context.Exception is ArgumentException)
        //    {
        //        var obj = JsonConvert.SerializeObject(new { error = context.Exception.Message });
        //        context.ExceptionHandled = true;
        //        context.Result = new BadRequestObjectResult(obj);
        //    }
        //}
    }
}
