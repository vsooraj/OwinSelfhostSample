using System.Security.Claims;

namespace OwinSelfhostSample.Models
{
    public interface IAccountRepository
    {
        bool ValidateCredentials(string domainName, string userName, string password, out ClaimsIdentity identity);
    }
}
