using Microsoft.AspNetCore.Mvc;
namespace Aramis.Api.ExceptionService.Interfaces
{
    public interface IExceptionService
    {
        IActionResult ReturnResult(Exception ex);
    }
}
