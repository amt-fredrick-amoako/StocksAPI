using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StocksAPI.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet("[action]")]
        public ActionResult Error()
        {
            IExceptionHandlerPathFeature? exceptionHandlerPathFeature = ControllerContext.HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
            {
                Error error = new Error(exceptionHandlerPathFeature.Error.Message);
                return new JsonResult(error);
            }
            else
            {
                Error error = new Error("Error encountered");
                return new JsonResult(error);
            }
        }
    }

    public record Error(string? errorMessage);
}
