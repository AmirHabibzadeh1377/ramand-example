using Asp.Versioning;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ramand_Application.Model;
using Ramand_Application.ServiceContract.Persistence;

using Ramand_Domain.Models;

namespace Ramand_Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : Controller
    {
        #region Fields

        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Ctor

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        #endregion

        [HttpPost("login")]
        public ActionResult<AuthResponse> Login([FromBody] AuthRequest request)
        {
            return Ok(_authenticationService.Login(request));
        }

        [Authorize]
        [HttpGet("users")]
        public ActionResult<User> GetUser()
        {
            return Ok(_authenticationService.GetUsers());
        }
    }
}
