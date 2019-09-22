using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPractice.Controllers
{
    public class ErrorController : Controller
    {
        private ILogger _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "The page you are looking for does not exists";
                    _logger.LogWarning($"Warning the page you are looking for {statusCodeResult.OriginalPath} does not exists Qery string values are {statusCodeResult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;
            _logger.LogError($"The path {exceptionHandlerPathFeature.Path} threw an error and the details are {exceptionHandlerPathFeature.Error}");
            return View("Error");
        }
    }
}
