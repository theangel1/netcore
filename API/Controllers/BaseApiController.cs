using Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly ILoggerService _logger;

        public BaseApiController(ILoggerService logger)
        {
            _logger = logger;

        }
        protected string GetControllerActionNames()
        {
            /*Porque este metodo? para examinar de forma rapida en el log desde donde sale 
            algun tipo de error*/

            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"[controller: {controller} method: {action}]";
        }

        protected ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Algo salió mal en el servidor. Por favor, contacte con el administrador.");
        }
    }
}