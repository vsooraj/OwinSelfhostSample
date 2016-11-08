using OwinSelfhostSample.Configuration;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;

namespace OwinSelfhostSample.Controllers
{
    public class AccountRepository
    {
        public bool ValidateCredentials(string domainName, string userName, string password, out ClaimsIdentity identity)
        {

           
            using (PrincipalContext pCtx = new PrincipalContext(ContextType.Domain, domainName))
            {
                bool isValid = pCtx.ValidateCredentials(userName, password);

                if (isValid)
                {
                    identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, userName));
                }
                else
                {
                    identity = null;
                }

                return isValid;
            }
        }


    }
}