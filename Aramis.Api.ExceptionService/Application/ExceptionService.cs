using Aramis.Api.ExceptionService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aramis.Api.ExceptionService.Application
{
    public class ExceptionService : IExceptionService
    {
        public ExceptionService()
        {
        }

        public IActionResult ReturnResult(Exception ex)
        {
            var Status = ex.GetType();
            string Messsage;
            int StatusCode;
          
            switch (Status.Name)
            {
                case "UnauthorizedAccessException":
                    {
                        StatusCode = 401;
                        Messsage = ex.Message.ToString();
                        break;
                    }
                default:
                    {
                        StatusCode = 400;
                        Messsage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        break;
                    }
            }
            return new ContentResult() { Content = Messsage, StatusCode = StatusCode };
        }
    }
}