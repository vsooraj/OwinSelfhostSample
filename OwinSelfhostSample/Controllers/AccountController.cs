﻿using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Newtonsoft.Json;
using OwinSelfhostSample.Configuration;
using OwinSelfhostSample.Models;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace OwinSelfhostSample.Controllers
{

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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

            var password = EncodedString(login.Password);

            var domainName = string.Empty; //EncodedString(System.Configuration.ConfigurationManager.AppSettings["DirectoryDomain"]);
            var host = miGuardConfig.Settings.API.Authentication.Parameters[0].Value;
            var port = miGuardConfig.Settings.API.Authentication.Parameters[1].Value;
            if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(port))
            {
                domainName = host.Trim() + ":" + port.Trim();
            }

            if (!_accountRepository.ValidateCredentials(domainName, login.UserName, password, out identity))
            {

                return BadRequest("Incorrect username or password");
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
        public IHttpActionResult Logout([FromBody]LoginViewModel login)
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }
        private string EncodedString(string plainText)
        {
            if (!string.IsNullOrEmpty(plainText))
                return Encoding.UTF8.GetString(Convert.FromBase64String(plainText));
            else
                return string.Empty;
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


}
