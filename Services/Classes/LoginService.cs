using OAuth2._0andOpenIDConnect.Services.Interfaces;

namespace OAuth2._0andOpenIDConnect.Services.Classes
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void Login()
        {
            var AppID= "747642700616-08lgar5pf04bm7branjj6h4lsnta1mbn.apps.googleusercontent.com";
            var googleAuthorization = "https://accounts.google.com/o/oauth2/auth?" +
                                      "scope=email profile openid" +
                                      $"&client_id={AppID}" +
                                      $"&response_type=code" +
                                      $"&redirect_uri={"https://localhost:7160/api/OAuth/RedirectPage"}";

            var response = _contextAccessor.HttpContext!.Response;
            _contextAccessor.HttpContext!.Response.Redirect(googleAuthorization);
        }
    }
}
