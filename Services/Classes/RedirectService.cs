using Microsoft.Extensions.Configuration;
using OAuth2._0andOpenIDConnect.Services.Interfaces;
using System.Text.Json;

namespace OAuth2._0andOpenIDConnect.Services.Classes
{
    public class GoogleToken
    {
        public string access_token { get; set; }
    }
    public class RedirectService : IRedirectService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public RedirectService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public void GetToken()
        {
            string code = _contextAccessor.HttpContext?.Request?.Query["code"]!;
            var AppID = "747642700616-08lgar5pf04bm7branjj6h4lsnta1mbn.apps.googleusercontent.com";
            var AppSecret = "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";
            var applicationRedirectUrl = "https://localhost:7160/api/OAuth/RedirectPage";
            var formatContent = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type","authorization_code"),
            new KeyValuePair<string, string>("client_id",AppID!),
            new KeyValuePair<string, string>("code",code!),
            new KeyValuePair<string, string>("client_secret",AppSecret!),
            new KeyValuePair<string, string>("redirect_uri",applicationRedirectUrl),
        });
            var access_token = "";
            using (HttpClient client = new HttpClient())
            {
                var response = client.PostAsync("https://oauth2.googleapis.com/token", formatContent).GetAwaiter().GetResult();
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var token = JsonSerializer.Deserialize<GoogleToken>(content);
                access_token = token!.access_token;
            }

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync($"https://www.googleapis.com/oauth2/v1/userinfo?access_token={access_token}").GetAwaiter().GetResult();
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
        }
    }
}
