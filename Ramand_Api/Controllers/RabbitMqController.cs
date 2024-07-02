using Asp.Versioning;

using Microsoft.AspNetCore.Mvc;

using Ramand_Application.ServiceContract.Persistence;

namespace Ramand_Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RabbitMqController : Controller
    {
        #region Fields

        private readonly IRabbitMqService _rabbitMqService;

        #endregion

        #region Ctor

        public RabbitMqController(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        #endregion

        #region Methods

        [HttpPost("SendUserNumberOne")]
        public IActionResult SendUserNumberOneToRabbitMq()
        {
            var user = _rabbitMqService.GetUserById(1);
            _rabbitMqService.SendUser(user);
            return Ok();
        }
        
        #endregion
    }
}