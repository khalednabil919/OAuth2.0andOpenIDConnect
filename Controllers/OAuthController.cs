using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuth2._0andOpenIDConnect.Services.Interfaces;

namespace OAuth2._0andOpenIDConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRedirectService _redirectService;
        public OAuthController(ILoginService loginService, IRedirectService redirectService)
        {
            _loginService = loginService;
            _redirectService = redirectService;
        }

        [HttpGet("RedirectPage")]
        public IActionResult RedirectPage()
        {

            _redirectService.GetToken();

            return Ok(new string("Redirect Strongly Succeeed"));
        }
        [HttpGet("LoginPage")]
        public IActionResult LoginPage()
        {
            _loginService.Login();
            return Ok();
        }
    }
}
