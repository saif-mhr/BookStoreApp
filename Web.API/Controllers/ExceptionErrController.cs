using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using App.Core.Util.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;

namespace Web.API.Controllers
{
    public class ExceptionErrController : Controller
    {
        public IConfiguration Configuration { get; }
        public ExceptionErrController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Route(template: "ExceptionErr")]
        [HttpGet]
        [AllowAnonymous]
        public StatusCodeResult ExceptionErr()
        {
            LogHelper.WriteLog(exception: HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error,
                               path: Configuration[key: "Exceptions:FilePath"]);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
