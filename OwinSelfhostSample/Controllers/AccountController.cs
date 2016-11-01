using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace OwinSelfhostSample.Controllers
{

    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AccountRepository accountRepository;
        // private readonly ILoginProvider _loginProvider;
        public AccountController()
        {

            accountRepository = new AccountRepository();
        }

        [AllowAnonymous]
        [HttpPost, Route("Token")]
        public IHttpActionResult Token([FromBody]LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {

                return this.BadRequest(ModelState);
            }

            ClaimsIdentity identity;
            //var userName = Encoding.UTF8.GetString(
            //     Convert.FromBase64String(login.UserName));
            var password = Encoding.UTF8.GetString(
                     Convert.FromBase64String(login.Password));


            if (!accountRepository.ValidateCredentials(login.UserName, password, out identity))
            {

                return BadRequest("Incorrect user or password");
            }

            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            var currentUtc = new SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));



            return Ok(new LoginAccessViewModel
            {
                UserName = login.UserName,
                AccessToken = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket)
            });
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }
        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        //[AllowAnonymous]
        //[Route("ExternalLogins")]
        //public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        //{
        //    IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
        //    List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

        //    string state;

        //    if (generateState)
        //    {
        //        const int strengthInBits = 256;
        //        state = RandomOAuthStateGenerator.Generate(strengthInBits);
        //    }
        //    else
        //    {
        //        state = null;
        //    }

        //    foreach (AuthenticationDescription description in descriptions)
        //    {
        //        ExternalLoginViewModel login = new ExternalLoginViewModel
        //        {
        //            Name = description.Caption,
        //            Url = Url.Route("ExternalLogin", new
        //            {
        //                provider = description.AuthenticationType,
        //                response_type = "token",
        //                client_id = Startup.PublicClientId,
        //                redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
        //                state = state
        //            }),
        //            State = state
        //        };
        //        logins.Add(login);
        //    }

        //    return logins;
        //}
        //private static class RandomOAuthStateGenerator
        //{
        //    private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

        //    public static string Generate(int strengthInBits)
        //    {
        //        const int bitsPerByte = 8;

        //        if (strengthInBits % bitsPerByte != 0)
        //        {
        //            throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
        //        }

        //        int strengthInBytes = strengthInBits / bitsPerByte;

        //        byte[] data = new byte[strengthInBytes];
        //        _random.GetBytes(data);
        //        return HttpServerUtility.UrlTokenEncode(data);
        //    }
        //}

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }




    }

    internal class LoginAccessViewModel
    {
        public string AccessToken { get; set; }
        public string UserName { get; set; }
    }

    public class LoginViewModel
    {
        [JsonProperty(Required = Required.Always)]
        public string UserName { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Password { get; set; }
    }

    //public class ExternalLoginViewModel
    //{
    //    public string Name { get; set; }

    //    public string Url { get; set; }

    //    public string State { get; set; }
    //}
}
