using App.Core.Util.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.UI.Controllers
{
    public class ExceptionErrController : Controller
    {
        public IConfiguration Configuration { get; }

        public ExceptionErrController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [AllowAnonymous]
        public ViewResult Error()
        {
            LogHelper.WriteLog(exception: HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error,
                               path: Configuration[key: "Exceptions:FilePath"]);
            return View();
        }
    }
}
